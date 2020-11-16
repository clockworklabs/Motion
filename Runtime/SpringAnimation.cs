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
                if (Playing) return;

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

        public SpringAnimation<T> SetDelay(float delay)
        {
            Delay = delay;

            return this;
        }

        public SpringAnimation<T> SetLoops(int loops, LoopType loopType = LoopType.Restart)
        {
            LoopsCount = loops;
            LoopType = loopType;

            return this;
        }

        public SpringAnimation<T> SetInterval(int interval, float delay = 0)
        {
            Interval = interval;
            IntervalDelay = delay;

            return this;
        }

        public SpringAnimation<T> SetSpring(Spring spring)
        {
            Spring = spring;

            return this;
        }

        public SpringAnimation<T> SetInitialVelocity(T velocity)
        {
            Velocity = velocity;

            return this;
        }

        public SpringAnimation<T> OnLoop(Action callback)
        {
            OnLoopCallback = callback;

            return this;
        }

        public SpringAnimation<T> OnInterval(Action callback)
        {
            OnIntervalCallback = callback;

            return this;
        }

        public SpringAnimation<T> OnComplete(Action callback)
        {
            OnCompleteCallback = callback;

            return this;
        }

        protected override bool Tick(float deltaTime) {
            if (LoopsCount == 0)
            {
                return true;
            }

            if (IsInterval)
            {
                if (Accum < IntervalDelay)
                {
                    Accum += deltaTime;
                    return false;
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
                    OnLoopCallback?.Invoke();
                    if (IsInterval)
                    {
                        OnIntervalCallback?.Invoke();
                    }
                    OnCompleteCallback?.Invoke();
                    return true;
                }
                
                if (LoopType == LoopType.PingPong)
                {
                    SwapOriginAndTarget();
                }
                
                Setter(Origin);
                OnLoopCallback?.Invoke();
                if (IsInterval)
                {
                    OnIntervalCallback?.Invoke();
                }
                
                return false;
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
            
            return false;
        }

        protected abstract T Add(T a, T b);
        protected abstract T Subtract(T a, T b);
        protected abstract T Multiply(T a, float b);
        protected abstract float SqrMagnitude(T a);
    }
}