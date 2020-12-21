using System;
using UnityEngine;

namespace Motion
{
    public class FloatInertia : Animation<float>
    {
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

        private float initialVelocity;
        public float InitialVelocity
        {
            get => initialVelocity;
            private set
            {
                if (Started) return;

                initialVelocity = value;
            }
        }

        private float min;
        public float Min
        {
            get => min;
            private set
            {
                if (Started) return;

                min = value;
            }
        }

        private float max;
        public float Max
        {
            get => max;
            private set
            {
                if (Started) return;

                max = value;
            }
        }
        
        public float Velocity { get; private set; }
        
        private bool Spring { get; set; }

        private Func<float> Getter { get; set; }
        private Action<float> Setter { get; set; }

        internal override void Reset()
        {
            base.Reset();
            
            SetInertia(Inertia.Default);

            Spring = false;

            InitialVelocity = 0f;
            Velocity = 0f;
            Min = float.MinValue;
            Max = float.MaxValue;
        }

        internal void Setup(Func<float> getter, Action<float> setter, float initialVelocity)
        {
            Getter = getter;
            Setter = setter;
            SetInitialVelocity(initialVelocity);

            Valid = true;
        }

        public override void Play()
        {
            var origin = Getter();
            
            var sign = Mathf.Sign(InitialVelocity);
            var diff = Mathf.Pow(Mathf.Abs(InitialVelocity), Inertia.power) * sign;

            var target = origin + diff;
            if (Mathf.Approximately(origin, target) && origin > Min && origin < Max)
            {
                Valid = false;
                return;
            }

            Setup(Getter, Setter, origin, target);
            
            base.Play();
        }

        public FloatInertia SetInertia(Inertia inertia)
        {
            Inertia = inertia;

            return this;
        }

        public FloatInertia SetInitialVelocity(float velocity)
        {
            InitialVelocity = velocity;
            Velocity = velocity;

            return this;
        }

        public FloatInertia SetMinBoundary(float boundary)
        {
            Min = boundary;

            return this;
        }

        public FloatInertia SetMaxBoundary(float boundary)
        {
            Max = boundary;

            return this;
        }

        public FloatInertia SetBoundaries(float min, float max)
        {
            SetMinBoundary(min);
            SetMaxBoundary(max);

            return this;
        }

        protected override bool Tick(float deltaTime, ref float value) {
            Spring |= value < Min || value > Max;
            
            float target;
            if (Spring)
            {
                target = value < Min ? Min : Max;
            }
            else
            {
                target = Target;
            }
            
            var delta = value - target;
            if (Mathf.Abs(Velocity) < Inertia.restSpeed && Mathf.Abs(delta) < Inertia.restDelta)
            {
                value = target;
                return true;
            }

            if (Spring)
            {
                var k = -Inertia.bounceStiffness;
                // Damping constant, in kg / s
                var d = -Inertia.bounceDamping;
        
                var fSpring = delta * k;
                var fDamping = Velocity * d;
                var a = (fSpring + fDamping) * Inertia.bounceInverseMass;
                Velocity += a * deltaTime;
            }
            else
            {
                var t = Mathf.InverseLerp(target, Origin, value);
                
                Velocity = InitialVelocity * Mathf.Pow(t, Inertia.power);
            }
            
            value += Velocity * deltaTime;
            if ((value < Min || value > Max) && Inertia.bounceInverseMass <= 0)
            {
                value = value < Min ? Min : Max;
                return true;
            }
            
            return false;
        }
    }
}