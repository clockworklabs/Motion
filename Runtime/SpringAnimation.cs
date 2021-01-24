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

        // parameters for closed form solution
        private float Omega { get; set; }
        private float Zeta { get; set; }
        private float OmegaZeta { get; set; }
        private T X0 { get; set; }
        private T V0 { get; set; }
        private float ElapsedTime { get; set; } = 0;
        private DampingProfile dampingProfile { get; set; }

        public T Velocity { get; private set; }

        internal override void Reset()
        {
            base.Reset();

            SetSpring(Spring.Default);
            Velocity = default;
            V0 = default;
        }

        internal void Setup(Func<T> getter, Action<T> setter, T target)
        {
            var origin = getter();
            if (origin.Equals(target)) return;

            // Setting parameters
            X0 = Subtract(target, origin);
            ElapsedTime = 0;

            Setup(getter, setter, origin, target);
        }

        public SpringAnimation<T> SetSpring(Spring spring)
        {
            Spring = spring;

            // Setting parameters
            Zeta = Spring.damping / (2 * Mathf.Sqrt(Spring.stiffness / Spring.inverseMass));
            Omega = Mathf.Sqrt(Spring.stiffness * Spring.inverseMass);
            OmegaZeta = Omega * Zeta;
            dampingProfile = DampingProfile.Underdamped;
            if (Mathf.Abs(1 - Zeta) < 0.01f)
            {
                dampingProfile = DampingProfile.CriticallyDamped;
            }
            else if (Zeta > 1)
            {
                dampingProfile = DampingProfile.Overdamped;
            }

            return this;
        }

        public SpringAnimation<T> SetInitialVelocity(T velocity)
        {
            if (Started) return this;

            Velocity = velocity;

            // Setting parameters
            V0 = Multiply(Velocity, -1);

            return this;
        }

        protected T TickOverDamped(float t)
        {
            var omega2 = Omega * Mathf.Sqrt(Zeta * Zeta - 1.0f);
            var z1 = -OmegaZeta - omega2;
            var z2 = -OmegaZeta + omega2;
            var e1 = Mathf.Exp(z1 * t);
            var e2 = Mathf.Exp(z2 * t);
            var c1 = Multiply(Subtract(V0, Multiply(X0, z2)), 1/(-2 * omega2));
            var c2 = Subtract(X0, c1);

            var x = Add(Multiply(c1, e1), Multiply(c2, e2));
            Velocity = Add(Multiply(c1, z1 * e1), Multiply(c2, z2 * e2));

            return Subtract(Target, x);
        }

        protected T TickUnderDamped(float t)
        {
            var omega1 = Omega * Mathf.Sqrt(1.0f - Zeta * Zeta);
            var e = Mathf.Exp(-OmegaZeta * t);
            var c1 = X0;
            var c2 = Multiply(Add(V0, Multiply(X0, OmegaZeta)), 1/omega1);
            var cos = Mathf.Cos(omega1 * t);
            var sin = Mathf.Sin(omega1 * t);
            var a = Multiply(Subtract(Multiply(X0, OmegaZeta), Multiply(c2, omega1)), cos);
            var b = Multiply(Add(Multiply(X0, omega1), Multiply(c2, OmegaZeta)), sin);

            var x = Multiply(Add(Multiply(c1, cos), Multiply(c2, sin)), e);
            Velocity = Multiply(Add(a, b), e);

            return Subtract(Target, x);
        }

        protected T TickCriticallyDamped(float t)
        {
            var e = Mathf.Exp(-Omega * t);

            var x = Multiply(Add(X0, Multiply(Add(V0, Multiply(X0, Omega)), t)), e);
            Velocity = Multiply(Add(Multiply(V0, (1 - t * Omega)), Multiply(X0, t * Omega * Omega)), e);

            return Subtract(Target, x);
        }

        protected override bool Tick(float deltaTime, ref T value)
        {
            var delta = Subtract(value, Target);
            if (SqrMagnitude(Velocity) < Spring.sqrRestSpeed && SqrMagnitude(delta) < Spring.sqrRestDelta)
            {
                value = Target;
                return true;
            }

            ElapsedTime += deltaTime;
            switch(dampingProfile)
            {
                case DampingProfile.Overdamped:
                    value = TickOverDamped(ElapsedTime);
                    break;
                case DampingProfile.Underdamped:
                    value = TickUnderDamped(ElapsedTime);
                    break;
                default:
                    value = TickCriticallyDamped(ElapsedTime);
                    break;
            }

            return false;
        }

        protected abstract T Add(T a, T b);
        protected abstract T Subtract(T a, T b);
        protected abstract T Multiply(T a, float b);
        protected abstract float SqrMagnitude(T a);
    }

    enum DampingProfile
    {
        Underdamped,
        Overdamped,
        CriticallyDamped
    }

}
