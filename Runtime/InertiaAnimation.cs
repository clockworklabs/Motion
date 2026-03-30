using System;
using UnityEngine;

namespace Motion
{
    public readonly struct InertiaAnimationId
    {
        public readonly ulong id;

        public InertiaAnimationId(ulong id)
        {
            this.id = id;
        }

        public static bool From(ulong id, out InertiaAnimationId result)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                result = new InertiaAnimationId(id);
                return true;
            }
            result = default;
            return false;
        }
        
        public bool HasStarted(out bool started)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
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
            if (animation is InertiaAnimation)
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
            if (animation is InertiaAnimation)
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
            if (animation is InertiaAnimation)
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
            if (animation is InertiaAnimation)
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
            if (animation is InertiaAnimation)
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
            if (animation is InertiaAnimation)
            {
                autoPlay = animation.AutoPlay;
                return true;
            }

            autoPlay = false;
            return false;
        }

        public InertiaAnimationId OnStart(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.OnStart(callback);
            }

            return this;
        }

        public InertiaAnimationId OnPlay(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.OnPlay(callback);
            }

            return this;
        }

        public InertiaAnimationId OnPause(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.OnPause(callback);
            }

            return this;
        }

        public InertiaAnimationId OnStop(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.OnStop(callback);
            }

            return this;
        }

        public InertiaAnimationId OnComplete(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.OnComplete(callback);
            }

            return this;
        }
        
        public void SetAutoPlay(bool autoPlay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.SetAutoPlay(autoPlay);
            }
        }
        
        public bool GetTarget(out float target)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation springAnimation)
            {
                target = springAnimation.Target;
                return true;
            }

            target = default;
            return false;
        }

        public bool GetDelay(out float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                delay = animation.Delay;
                return true;
            }

            delay = 0f;
            return false;
        }

        public void SetDelay(float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.SetDelay(delay);
            }
        }

        public void Play()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.Play();
            }
        }

        public void Pause()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.Pause();
            }
        }

        public void Stop(bool complete = false)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.Stop(complete);
            }
        }

        public InertiaAnimationId OnStep(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation)
            {
                animation.OnStep(callback);
            }

            return this;
        }
        
        public bool GetInitialVelocity(out float initialVelocity)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation inertiaAnimation)
            {
                initialVelocity = inertiaAnimation.InitialVelocity;
                return true;
            }

            initialVelocity = 0f;
            return false;
        }
        
        public bool GetMin(out float min)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation inertiaAnimation)
            {
                min = inertiaAnimation.Min;
                return true;
            }

            min = 0f;
            return false;
        }
        
        public bool GetMax(out float max)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation inertiaAnimation)
            {
                max = inertiaAnimation.Max;
                return true;
            }

            max = 0f;
            return false;
        }
        
        public bool GetVelocity(out float velocity)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation inertiaAnimation)
            {
                velocity = inertiaAnimation.Velocity;
                return true;
            }

            velocity = 0f;
            return false;
        }

        public InertiaAnimationId SetInitialVelocity(float initialVelocity)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation inertiaAnimation)
            {
                inertiaAnimation.SetInitialVelocity(initialVelocity);
            }

            return this;
        }

        public InertiaAnimationId SetInertia(Inertia inertia)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation inertiaAnimation)
            {
                inertiaAnimation.SetInertia(inertia);
            }

            return this;
        }

        public InertiaAnimationId SetMinBoundary(float boundary)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation inertiaAnimation)
            {
                inertiaAnimation.SetMinBoundary(boundary);
            }

            return this;
        }
        
        public InertiaAnimationId SetMaxBoundary(float boundary)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation inertiaAnimation)
            {
                inertiaAnimation.SetMaxBoundary(boundary);
            }

            return this;
        }
        
        public InertiaAnimationId SetBoundaries(float min, float max)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is InertiaAnimation inertiaAnimation)
            {
                inertiaAnimation.SetBoundaries(min, max);
            }

            return this;
        }
        
        public static implicit operator ulong(InertiaAnimationId animation) => animation.id;
        public static implicit operator AnimationId(InertiaAnimationId animation) => new AnimationId(animation.id);
    }
    
    public class InertiaAnimation : Animation
    {
        private Func<float> Getter { get; set; }
        private Action<float> Setter { get; set; }
        
        private float Origin { get; set; }
        public float Target { get; private set; }
        
        private Action _onStepCallback;
        internal Action OnStepCallback {
            get => _onStepCallback;
            private set
            {
                if (Started) return;
                
                _onStepCallback = value;
            }
        }
        
        private Inertia _inertia;
        internal Inertia Inertia
        {
            get => _inertia;
            private set
            {
                if (Started) return;

                _inertia = value;
            }
        }

        private float Omega { get; set; }
        private float Zeta { get; set; }
        private float OmegaZeta { get; set; }
        private float X0 { get; set; }
        private float V0 { get; set; }
        private float ElapsedTime { get; set; }
        private DampingProfile DampingProfile { get; set; }

        private float _initialVelocity;
        internal float InitialVelocity
        {
            get => _initialVelocity;
            private set
            {
                if (Started) return;

                _initialVelocity = value;
            }
        }

        private float _min;
        internal float Min
        {
            get => _min;
            private set
            {
                if (Started) return;

                _min = value;
            }
        }

        private float _max;
        internal float Max
        {
            get => _max;
            private set
            {
                if (Started) return;

                _max = value;
            }
        }
        
        internal float Velocity { get; private set; }
        
        private bool Spring { get; set; }

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

        internal void Setup(Func<float> getter, Action<float> setter)
        {
            Getter = getter;
            Setter = setter;
        }

        public override void Play()
        {
            base.Play();
            
            Origin = Getter();
            
            Target = Origin + InitialVelocity * Inertia.power;
            
            if (Mathf.Approximately(Origin, Target) && Origin > Min && Origin < Max)
            {
                Stop(false);
            }
        }

        protected override void OnStop(bool complete)
        {
            if (!complete) return;

            Setter(Target);
        }

        public void SetInitialVelocity(float velocity)
        {
            if (Started) return;
            
            InitialVelocity = velocity;
            Velocity = velocity;
        }

        public void SetInertia(Inertia inertia)
        {
            if (Started) return;
            
            Inertia = inertia;
            
            Zeta = Inertia.bounceDamping / (2 * Mathf.Sqrt(Inertia.bounceStiffness / Inertia.bounceInverseMass));
            Omega = Mathf.Sqrt(Inertia.bounceStiffness * Inertia.bounceInverseMass);
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

        public void SetMinBoundary(float boundary) => Min = boundary;
        public void SetMaxBoundary(float boundary) => Max = boundary;
        public void SetBoundaries(float min, float max)
        {
            SetMinBoundary(min);
            SetMaxBoundary(max);
        }

        protected override bool Tick(float deltaTime)
        {
            var value = Getter();
            var done = Tick(deltaTime, ref value);
            
            OnStepCallback?.Invoke();
            
            Setter(value);

            return done;
        }

        private bool Tick(float deltaTime, ref float value)
        {
            var prevSpring = Spring;
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
                if (!prevSpring)
                {
                    X0 = target - value;
                    V0 = -Velocity;
                    ElapsedTime = 0;
                }

                ElapsedTime += deltaTime;
                switch(DampingProfile)
                {
                    case DampingProfile.OverDamped:
                        value = TickOverDamped(ElapsedTime, target);
                        break;
                    case DampingProfile.UnderDamped:
                        value = TickUnderDamped(ElapsedTime, target);
                        break;
                    case DampingProfile.CriticallyDamped:
                        value = TickCriticallyDamped(ElapsedTime, target);
                        break;
                    default:
                        throw new UnityException("Damping profile not supported");
                }
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

        private float TickOverDamped(float t, float target)
        {
            var omega2 = Omega * Mathf.Sqrt(Zeta * Zeta - 1.0f);
            var z1 = -OmegaZeta - omega2;
            var z2 = -OmegaZeta + omega2;
            var e1 = Mathf.Exp(z1 * t);
            var e2 = Mathf.Exp(z2 * t);
            var c1 = (V0 - X0 * z2) * (1 / (-2 * omega2));
            var c2 = X0 - c1;

            var x = c1 * e1 + c2 * e2;
            Velocity = c1 * z1 * e1 + c2 * z2 * e2;

            return target - x;
        }

        private float TickUnderDamped(float t, float target)
        {
            var omega1 = Omega * Mathf.Sqrt(1.0f - Zeta * Zeta);
            var e = Mathf.Exp(-OmegaZeta * t);
            var c1 = X0;
            var c2 = (V0 + X0 * OmegaZeta) * (1 / omega1);
            var cos = Mathf.Cos(omega1 * t);
            var sin = Mathf.Sin(omega1 * t);
            var a = (X0 + OmegaZeta - (c2 + omega1)) * cos;
            var b = (X0 * omega1 + c2 * OmegaZeta) * sin;

            var x = (c1 * cos + c2 * sin) * e;
            Velocity = (a + b) * e;

            return target - x;
        }

        private float TickCriticallyDamped(float t, float target)
        {
            var e = Mathf.Exp(-Omega * t);

            var x = (X0 + (V0 + X0 * Omega) * t) * e;
            Velocity = (V0 * (1 - t * Omega) + X0 * (t * Omega * Omega)) * e;

            return target - x;
        }
    }
}