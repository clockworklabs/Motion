using System;
using UnityEngine;

namespace Motion
{
    public abstract class InertiaAnimation<T> : Animation<T> where T : struct, IEquatable<T>
    {
        protected abstract int Dimensions { get; }
        protected abstract T Zero { get; }
        protected abstract T MinValue { get; }
        protected abstract T MaxValue { get; }
        protected abstract float Get(T value, int dimension);
        protected abstract void Set(ref T value, int dimension, float component);

        internal T GetTarget(T origin, T initialVelocity)
        {
            T target = default;
            for (int i = 0, n = Dimensions; i < n; i++)
            {
                var cOrigin = Get(origin, i);
                var cInitialVelocity = Get(initialVelocity, i);
                var sign = Mathf.Sign(cInitialVelocity);
                var power = 0.8f;
                var diff = Mathf.Pow(Mathf.Abs(cInitialVelocity), power) * sign;
                
                Set(ref target, i, cOrigin + diff);
            }

            return target;
        }
        
        private Inertia inertia;
        public Inertia Inertia
        {
            get => inertia;
            private set
            {
                if (Started) return;

                inertia = value;
            }
        }

        private T initialVelocity;
        public T InitialVelocity
        {
            get => initialVelocity;
            private set
            {
                if (Started) return;

                initialVelocity = value;
            }
        }

        private T min;
        public T Min
        {
            get => min;
            private set
            {
                if (Started) return;

                min = value;
            }
        }

        private T max;
        public T Max
        {
            get => max;
            private set
            {
                if (Started) return;

                max = value;
            }
        }
        
        public T Velocity { get; private set; }

        protected override bool Check()
        {
            for (int i = 0, n = Dimensions; i < n; i++)
            {
                var origin = Get(Origin, i);
                var target = Get(Target, i);
                var min = Get(Min, i);
                var max = Get(Max, i);
                Debug.Log($"{origin} - {target} - {min} - {max}");
                if (!Mathf.Approximately(origin, target) || origin < min || origin > max)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void Setup()
        {
            SetInertia(Inertia.Default);

            InitialVelocity = Zero;
            Velocity = Zero;
            Min = MinValue;
            Max = MaxValue;
        }

        public InertiaAnimation<T> SetInertia(Inertia inertia)
        {
            Inertia = inertia;

            return this;
        }

        public InertiaAnimation<T> SetInitialVelocity(T velocity)
        {
            InitialVelocity = velocity;
            Velocity = velocity;

            return this;
        }

        public InertiaAnimation<T> SetMinBoundary(T boundary)
        {
            Min = boundary;

            return this;
        }

        public InertiaAnimation<T> SetMaxBoundary(T boundary)
        {
            Max = boundary;

            return this;
        }

        public InertiaAnimation<T> SetBoundaries(T min, T max)
        {
            SetMinBoundary(min);
            SetMaxBoundary(max);

            return this;
        }

        protected override bool Tick(float deltaTime, ref T value) {
            var velocity = Velocity;
            var done = true;
            for (int i = 0, n = Dimensions; i < n; i++)
            {
                done &= Animate(ref value, ref velocity, i, deltaTime);
            }

            Velocity = velocity;

            return done;
        }

        private bool Animate(ref T value, ref T velocity, int dimension, float deltaTime)
        {
            var val = Get(value, dimension);
            var vel = Get(velocity, dimension);
            
            var min = Get(Min, dimension);
            var max = Get(Max, dimension);
            var spring = val < min || val > max;

            float target;
            if (spring)
            {
                target = val < min ? min : max;
            }
            else
            {
                target = Get(Target, dimension);
            }
            
            var delta = val - target;
            if (Mathf.Abs(vel) < Inertia.restSpeed && Mathf.Abs(delta) < Inertia.restDelta)
            {
                return true;
            }

            if (spring)
            {
                var k = -Inertia.bounceStiffness;
                // Damping constant, in kg / s
                var d = -Inertia.bounceDamping;
        
                var fSpring = delta * k;
                var fDamping = vel * d;
                var a = (fSpring + fDamping) * Inertia.bounceInverseMass;
                vel += a * deltaTime;
            }
            else
            {
                var origin = Get(Origin, dimension);
                var initialVelocity = Get(InitialVelocity, dimension);
                var t = Mathf.InverseLerp(target, origin, val);
                
                vel = initialVelocity * Mathf.Pow(t, Inertia.power);
            }
            
            val += vel * deltaTime;
            
            Set(ref velocity, dimension, vel);
            Set(ref value, dimension, val);

            return false;
        }
    }
}