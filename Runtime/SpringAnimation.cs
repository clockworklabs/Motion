using System;
using UnityEngine;

namespace Motion
{
    public readonly struct SpringAnimationId<T> where T : struct, IEquatable<T>
    {
        public readonly ulong id;

        public SpringAnimationId(ulong id)
        {
            this.id = id;
        }

        public static bool From(ulong id, out SpringAnimationId<T> result)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                result = new SpringAnimationId<T>(id);
                return true;
            }
            result = default;
            return false;
        }
        
        public bool HasStarted(out bool started)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                started = animation.Started;
                return true;
            }

            started = false;
            return false;
        }
        public bool IsPlaying(out bool playing)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                playing = animation.Playing;
                return true;
            }

            playing = false;
            return false;
        }
        public bool IsStopped(out bool stopped)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                stopped = animation.Stopped;
                return true;
            }

            stopped = false;
            return false;
        }
        public bool IsCompleted(out bool completed)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                completed = animation.Completed;
                return true;
            }

            completed = false;
            return false;
        }
        public bool IsActive(out bool active)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                active = animation.Active;
                return true;
            }

            active = false;
            return false;
        }

        public bool IsPaused(out bool paused)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                paused = animation.Paused;
                return true;
            }

            paused = false;
            return false;
        }

        public bool IsAutoPlay(out bool autoPlay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                autoPlay = animation.AutoPlay;
                return true;
            }

            autoPlay = false;
            return false;
        }

        public SpringAnimationId<T> OnStart(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.OnStart(callback);
            }

            return this;
        }

        public SpringAnimationId<T> OnPlay(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.OnPlay(callback);
            }

            return this;
        }

        public SpringAnimationId<T> OnPause(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.OnPause(callback);
            }

            return this;
        }

        public SpringAnimationId<T> OnStop(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.OnStop(callback);
            }

            return this;
        }

        public SpringAnimationId<T> OnComplete(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.OnComplete(callback);
            }

            return this;
        }

        public bool GetDelay(out float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                delay = animation.Delay;
                return true;
            }

            delay = 0f;
            return false;
        }
        
        public SpringAnimationId<T> SetAutoPlay(bool autoPlay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.SetAutoPlay(autoPlay);
            }

            return this;
        }

        public SpringAnimationId<T> SetDelay(float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.SetDelay(delay);
            }

            return this;
        }

        public void Play()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.Play();
            }
        }

        public void Pause()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.Pause();
            }
        }

        public SpringAnimationId<T> Stop(bool complete = false)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.Stop(complete);
            }

            return this;
        }

        public SpringAnimationId<T> OnStep(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T>)
            {
                animation.OnStep(callback);
            }

            return this;
        }

        public SpringAnimationId<T> OnLoop(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T> springAnimation)
            {
                springAnimation.OnLoop(callback);
            }

            return this;
        }

        public SpringAnimationId<T> OnInterval(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T> springAnimation)
            {
                springAnimation.OnInterval(callback);
            }

            return this;
        }

        public SpringAnimationId<T> SetLoops(int loops, LoopType type = LoopType.Restart)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T> springAnimation)
            {
                springAnimation.SetLoops(loops, type);
            }

            return this;
        }

        public SpringAnimationId<T> SetInterval(int interval, float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T> springAnimation)
            {
                springAnimation.SetInterval(interval, delay);
            }

            return this;
        }
        
        public bool GetTarget(out T target)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T> springAnimation)
            {
                target = springAnimation.Target;
                return true;
            }

            target = default;
            return false;
        }

        public bool GetSpring(out Spring spring)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T> springAnimation)
            {
                spring = springAnimation.Spring;
                return true;
            }

            spring = default;
            return false;
        }

        public bool GetVelocity(out T velocity)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T> springAnimation)
            {
                velocity = springAnimation.Velocity;
                return true;
            }

            velocity = default;
            return false;
        }
        
        public SpringAnimationId<T> SetTarget(T target)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T> springAnimation)
            {
                springAnimation.SetTarget(target);
            } 

            return this;
        }

        public SpringAnimationId<T> SetSpring(Spring spring)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T> springAnimation)
            {
                springAnimation.SetSpring(spring);
            } 

            return this;
        }

        public SpringAnimationId<T> SetInitialVelocity(T velocity)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is SpringAnimation<T> springAnimation)
            {
                springAnimation.SetInitialVelocity(velocity);
            } 

            return this;
        }
        
        public static implicit operator ulong(SpringAnimationId<T> animation) => animation.id;
        public static implicit operator AnimationId<T>(SpringAnimationId<T> animation) => new AnimationId<T>(animation.id);
        public static implicit operator AnimationId(SpringAnimationId<T> animation) => new AnimationId(animation.id);
    }
    
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
        private float ElapsedTime { get; set; }
        private DampingProfile DampingProfile { get; set; }

        internal override void Reset()
        {
            base.Reset();

            SetSpring(Spring.Default);
            V0 = default;
            ElapsedTime = 0;
        }

        internal override void Setup(Func<T> getter, Action<T> setter, T target)
        {
            base.Setup(getter, setter, target);
            
            X0 = Subtract(target, Origin);
            V0 = Multiply(Velocity, -1);
        }

        protected override void PrepareForLoop()
        {
            X0 = Subtract(Target, Origin);
            ElapsedTime = 0;
        }

        internal void SetTarget(T target)
        {
            ElapsedTime = 0;

            Setup(Getter, Setter, target);
        }

        internal void SetSpring(Spring spring)
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
        }

        internal void SetInitialVelocity(T velocity)
        {
            if (Started) return;

            Velocity = velocity;

            V0 = Multiply(Velocity, -1);
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
