using System;

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
        
        protected override bool Check() => !Origin.Equals(Target);

        protected override void Setup()
        {
            SetSpring(Spring.Default);

            Velocity = default;
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

        protected override bool Tick(float deltaTime, ref T value) {
            // Object position and velocity.
            var delta = Subtract(value, Target);
            if (SqrMagnitude(Velocity) < Spring.sqrRestSpeed && SqrMagnitude(delta) < Spring.sqrRestDelta)
            {
                value = Target;
                return true;
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

            return false;
        }

        protected abstract T Add(T a, T b);
        protected abstract T Subtract(T a, T b);
        protected abstract T Multiply(T a, float b);
        protected abstract float SqrMagnitude(T a);
    }
}