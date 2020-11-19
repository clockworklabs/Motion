using System;
using UnityEngine;

namespace Motion
{
    public abstract class SpringAnimation<T> : Animation<T> where T : struct, IEquatable<T>
    {
        private Spring spring;
        public Spring Spring
        {
            get => spring;
            private set
            {
                if (Started) return;

                spring = value;
            }
        }
        
        public T Velocity { get; private set; }
        
        private bool IsInterval { get; set; }
        private float Accum { get; set; }
        
        protected override void Setup()
        {
            SetSpring(Spring.Default);
            
            IsInterval = false;
            Accum = 0;
        }

        public SpringAnimation<T> SetSpring(Spring spring)
        {
            Spring = spring;

            return this;
        }

        public SpringAnimation<T> SetInitialVelocity(T velocity)
        {
            if (Started) return this;
            
            Velocity = velocity;

            return this;
        }

        protected override void OnStop(bool complete)
        {
            if (!complete) return;

            Setter(Target);
        }

        protected override TickResult Tick(float deltaTime) {
            if (LoopsCount == 0)
            {
                return new TickResult
                {
                    complete = true
                };
            }

            if (IsInterval)
            {
                if (Accum < IntervalDelay)
                {
                    Accum += deltaTime;
                    return new TickResult();
                }

                IsInterval = false;
                Accum = 0;
            }

            // Object position and velocity.
            var value = Getter();
            var delta = Subtract(value, Target);
            if (SqrMagnitude(Velocity) < Spring.sqrRestSpeed && SqrMagnitude(delta) < Spring.sqrRestDelta)
            {
                Loop++;
                IsInterval = Interval > 0 && Loop % Interval == 0;

                if (LoopsCount > 0 && Loop >= LoopsCount)
                {
                    Setter(Target);
                    
                    return new TickResult
                    {
                        loop = true,
                        interval = IsInterval,
                        complete = true
                    };
                }
                
                if (LoopType == LoopType.PingPong)
                {
                    SwapOriginAndTarget();
                }
                
                Setter(Origin);

                return new TickResult
                {
                    loop = true,
                    interval = IsInterval
                };
            }
            
            // Spring stiffness, in kg / s^2
            var k = -Spring.stiffness;
            // Damping constant, in kg / s
            var d = -Spring.damping;
        
            var fSpring = Multiply(delta, k);
            var fDamping = Multiply(Velocity, d);
            var a = Multiply(Add(fSpring, fDamping), Spring.inverseMass);
            Velocity = Add(Velocity, Multiply(a, deltaTime));
            value = Add(value, Multiply(Velocity, deltaTime));
            
            Setter(value);
            
            return new TickResult();
        }

        protected abstract T Add(T a, T b);
        protected abstract T Subtract(T a, T b);
        protected abstract T Multiply(T a, float b);
        protected abstract float SqrMagnitude(T a);
    }
}