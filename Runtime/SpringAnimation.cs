using System;
using UnityEngine;

namespace Motion
{
    public abstract class SpringAnimation<T> : Animation<T> where T : struct, IEquatable<T>
    {
        private Spring _spring;
        public Spring Spring
        {
            get => _spring;
            private set
            {
                if (Started) return;

                _spring = value;
            }
        }

        private float Omega { get; set; }
        private float Zeta { get; set; }
        private float OmegaZeta { get; set; }
        private T X0 { get; set; }
        private T V0 { get; set; }
        private float ElapsedTime { get; set; } = 0;
        private DampingProfile DampingProfile { get; set; }

        public T Velocity { get; private set; }

        internal override void Reset()
        {
            base.Reset();

            SetSpring(Spring.Default);
            Velocity = default;
            V0 = default;
            ElapsedTime = 0;
        }

        internal void Setup(Func<T> getter, Action<T> setter, T target)
        {
            var origin = getter();
            if (origin.Equals(target)) return;

            X0 = Subtract(target, origin);

            Setup(getter, setter, origin, target);
        }

        protected override void PrepareForLoop()
        {
            X0 = Subtract(Target, Origin);
            ElapsedTime = 0;
        }

        public SpringAnimation<T> SetSpring(Spring spring)
        {
            Spring = spring;

            Zeta = Spring.damping / (2 * Mathf.Sqrt(Spring.stiffness / Spring.inverseMass));
            Omega = Mathf.Sqrt(Spring.stiffness * Spring.inverseMass);
            OmegaZeta = Omega * Zeta;
            
            if (Mathf.Abs(1 - Zeta) < 0.01f)
            {
                DampingProfile = DampingProfile.CriticallyDamped;
            }
            else if (Zeta > 1)
            {
                DampingProfile = DampingProfile.OverDamped;
            }
            else
            {
                DampingProfile = DampingProfile.UnderDamped;
            }

            return this;
        }

        public SpringAnimation<T> SetInitialVelocity(T velocity)
        {
            if (Started) return this;

            Velocity = velocity;

            V0 = Multiply(Velocity, -1);

            return this;
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
            switch(DampingProfile)
            {
                case DampingProfile.OverDamped:
                    value = TickOverDamped(ElapsedTime);
                    break;
                case DampingProfile.UnderDamped:
                    value = TickUnderDamped(ElapsedTime);
                    break;
                case DampingProfile.CriticallyDamped:
                    value = TickCriticallyDamped(ElapsedTime);
                    break;
                default:
                    throw new UnityException("Damping profile not supported");
            }

            return false;
        }

        private T TickOverDamped(float t)
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

        private T TickUnderDamped(float t)
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

        private T TickCriticallyDamped(float t)
        {
            var e = Mathf.Exp(-Omega * t);

            var x = Multiply(Add(X0, Multiply(Add(V0, Multiply(X0, Omega)), t)), e);
            Velocity = Multiply(Add(Multiply(V0, (1 - t * Omega)), Multiply(X0, t * Omega * Omega)), e);

            return Subtract(Target, x);
        }

        protected abstract T Add(T a, T b);
        protected abstract T Subtract(T a, T b);
        protected abstract T Multiply(T a, float b);
        protected abstract float SqrMagnitude(T a);
    }

    internal enum DampingProfile
    {
        UnderDamped,
        OverDamped,
        CriticallyDamped
    }

}
