using System;
using UnityEngine;

namespace Motion
{
    
    public readonly struct TweenAnimationId<T> where T : struct, IEquatable<T>
    {
        public readonly uint id;

        public TweenAnimationId(uint id)
        {
            this.id = id;
        }
        
        public bool HasStarted(out bool started)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
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
            if (animation is TweenAnimation<T>)
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
            if (animation is TweenAnimation<T>)
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
            if (animation is TweenAnimation<T>)
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
            if (animation is TweenAnimation<T>)
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
            if (animation is TweenAnimation<T>)
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
            if (animation is TweenAnimation<T>)
            {
                autoPlay = animation.AutoPlay;
                return true;
            }

            autoPlay = false;
            return false;
        }

        public TweenAnimationId<T> OnStart(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.OnStart(callback);
            }

            return this;
        }

        public TweenAnimationId<T> OnPlay(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.OnPlay(callback);
            }

            return this;
        }

        public TweenAnimationId<T> OnPause(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.OnPause(callback);
            }

            return this;
        }

        public TweenAnimationId<T> OnStop(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.OnStop(callback);
            }

            return this;
        }

        public TweenAnimationId<T> OnComplete(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.OnComplete(callback);
            }

            return this;
        }

        public bool GetDelay(out float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                delay = animation.Delay;
                return true;
            }

            delay = 0f;
            return false;
        }
        
        public TweenAnimationId<T> SetAutoPlay(bool autoPlay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.SetAutoPlay(autoPlay);
            }

            return this;
        }

        public TweenAnimationId<T> SetDelay(float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.SetDelay(delay);
            }

            return this;
        }

        public void Play()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.Play();
            }
        }

        public void Pause()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.Pause();
            }
        }

        public void Stop(bool complete = false)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.Stop(complete);
            }
        }

        public TweenAnimationId<T> OnStep(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T>)
            {
                animation.OnStep(callback);
            }

            return this;
        }

        public TweenAnimationId<T> OnLoop(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T> tweenAnimation)
            {
                tweenAnimation.OnLoop(callback);
            }

            return this;
        }

        public TweenAnimationId<T> OnInterval(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T> tweenAnimation)
            {
                tweenAnimation.OnInterval(callback);
            }

            return this;
        }

        public TweenAnimationId<T> SetLoops(int loops, LoopType type = LoopType.Restart)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T> tweenAnimation)
            {
                tweenAnimation.SetLoops(loops, type);
            }

            return this;
        }

        public TweenAnimationId<T> SetInterval(int interval, float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T> tweenAnimation)
            {
                tweenAnimation.SetInterval(interval, delay);
            }

            return this;
        }

        public bool GetTween(out Tween tween)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T> tweenAnimation)
            {
                tween = new Tween
                {
                    duration = tweenAnimation.Duration,
                    ease = tweenAnimation.Ease
                };
                return true;
            }

            tween = default;
            return false;
        }

        public bool GetEase(out Ease ease)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T> tweenAnimation)
            {
                ease = tweenAnimation.Ease;
                return true;
            }

            ease = default;
            return false;
        }

        public bool GetDuration(out float duration)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T> tweenAnimation)
            {
                duration = tweenAnimation.Duration;
                return true;
            }

            duration = 0f;
            return false;
        }
        
        public TweenAnimationId<T> SetTween(Tween tween)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T> tweenAnimation)
            {
                tweenAnimation.SetTween(tween);
            }

            return this;
        }

        public TweenAnimationId<T> SetEase(Ease ease)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T> tweenAnimation)
            {
                tweenAnimation.SetEase(ease);
            }

            return this;
        }
        
        public TweenAnimationId<T> SetDuration(float duration)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is TweenAnimation<T> tweenAnimation)
            {
                tweenAnimation.SetDuration(duration);
            }

            return this;
        }
        
        public static implicit operator uint(TweenAnimationId<T> animation) => animation.id;
        public static implicit operator AnimationId<T>(TweenAnimationId<T> animation) => new AnimationId<T>(animation.id);
        public static implicit operator AnimationId(TweenAnimationId<T> animation) => new AnimationId(animation.id);
    }
    
    public abstract class TweenAnimation<T> : Animation<T> where T : struct, IEquatable<T>
    {
        private const float C1 = 1.70158f;
        private const float C2 = C1 * 1.525f;
        private const float C3 = C1 + 1;
        private const float C4 =  (2 * Mathf.PI) / 3;
        private const float C5 =  (2 * Mathf.PI) / 4.5f;
        private const float N1 =  7.5625f;
        private const float D1 =  1 / 2.75f;
            
        private float _inverseDuration;
        private float InverseDuration
        {
            get => _inverseDuration;
            set
            {
                if (Started) return;
                _inverseDuration = value;
            }
        }

        internal float Duration => 1 / InverseDuration;

        private Ease _ease;
        internal Ease Ease
        {
            get => _ease;
            private set
            {
                if (Started) return;
                _ease = value;
            }
        }

        private float Time { get; set; }

        internal override void Reset()
        {
            base.Reset();
            
            SetTween(Tween.Default);
            Time = 0;
        }

        protected override void PrepareForLoop()
        {
            Time = 0;
        }

        internal void SetTween(Tween tween)
        {
            SetEase(tween.ease);
            SetDuration(tween.duration);
        }

        internal void SetEase(Ease ease) => Ease = ease;
        internal void SetDuration(float duration) => InverseDuration = 1 / duration;

        protected override bool Tick(float deltaTime, ref T value) {
            Time = Mathf.Clamp01(Time + deltaTime * InverseDuration);
            
            if (Time >= 1)
            {
                value = Target;
                Velocity = default;
                return true;
            }

            float t;
            switch (Ease)
            {
                case Ease.Linear:
                    t = Time;
                    break;
                case Ease.SineIn:
                    t = 1 - Mathf.Cos(Time * Mathf.PI * 0.5f);
                    break;
                case Ease.Sine:
                    t = -(Mathf.Cos(Time * Mathf.PI) - 1) * 0.5f;
                    break;
                case Ease.SineOut:
                    t = Mathf.Sin(Time * Mathf.PI * 0.5f);
                    break;
                case Ease.QuadIn:
                    t = Time * Time;
                    break;
                case Ease.Quad:
                    t = Time < 0.5f 
                        ? 2 * Time * Time
                        : 1 - (2 - 2 * Time) * (2 - 2 * Time) * 0.5f;
                    break;
                case Ease.QuadOut:
                    t = 1 - (1 - Time) * (1 - Time);
                    break;
                case Ease.CubicIn:
                    t = Time * Time * Time;
                    break;
                case Ease.Cubic:
                    t = Time < 0.5f
                        ? 4 * Time * Time * Time 
                        : 1 - (2 - 2 * Time) * (2 - 2 * Time) * (2 - 2 * Time) * 0.5f;
                    break;
                case Ease.CubicOut:
                    t = 1 - (1 - Time) * (1 - Time) * (1 - Time);
                    break;
                case Ease.QuartIn:
                    t = Time * Time * Time * Time;
                    break;
                case Ease.Quart:
                    t = Time < 0.5f 
                        ? 8 * Time * Time * Time * Time 
                        : 1 - (2 - 2 * Time) * (2 - 2 * Time) * (2 - 2 * Time) * (2 - 2 * Time) * 0.5f;
                    break;
                case Ease.QuartOut:
                    t = 1 - (1 - Time) * (1 - Time) * (1 - Time) * (1 - Time);
                    break;
                case Ease.QuintIn:
                    t = Time * Time * Time * Time * Time;
                    break;
                case Ease.Quint:
                    t = Time < 0.5f 
                        ? 16 * Time * Time * Time * Time * Time 
                        : 1 - (2 - 2 * Time) * (2 - 2 * Time) * (2 - 2 * Time) * (2 - 2 * Time) * (2 - 2 * Time) * 0.5f;
                    break;
                case Ease.QuintOut:
                    t = 1 - (1 - Time) * (1 - Time) * (1 - Time) * (1 - Time) * (1 - Time);
                    break;
                case Ease.ExpoIn:
                    t = Time <= 0 ? 0 : Mathf.Pow(2, 10 * Time - 10);
                    break;
                case Ease.Expo:
                    t = Time <= 0 
                        ? 0 
                        : Time >= 1 
                            ? 1
                            : Time < 0.5f 
                                ? Mathf.Pow(2, 20 * Time - 10) * 0.5f
                                : (2 - Mathf.Pow(2, -20 * Time + 10)) * 0.5f;
                    break;
                case Ease.ExpoOut:
                    t = Time >= 1 ? 1 : 1 - Mathf.Pow(2, -10 * Time);
                    break;
                case Ease.CircleIn:
                    t = 1 - Mathf.Sqrt(1 - Time * Time);
                    break;
                case Ease.Circle:
                    t = Time < 0.5
                        ? (1 - Mathf.Sqrt(1 - (2 * Time) * (2 * Time))) * 0.5f
                        : (Mathf.Sqrt(1 - (-2 * Time + 2) * (-2 * Time + 2)) + 1) * 0.5f;
                    break;
                case Ease.CircleOut:
                    t = Mathf.Sqrt(1 - (Time - 1) * (Time - 1));
                    break;
                case Ease.BackIn:
                    t = C3 * Time * Time * Time - C1 * Time * Time;
                    break;
                case Ease.Back:
                    t = Time < 0.5 
                        ? ((2 * Time) * (2 * Time) * ((C2 + 1) * 2 * Time - C2)) * 0.5f
                        : ((2 * Time - 2) * (2 * Time - 2) * ((C2 + 1) * (Time * 2 - 2) + C2) + 2) * 0.5f;
                    break;
                case Ease.BackOut:
                    t = 1 + C3 * (Time - 1) * (Time - 1) * (Time - 1) + C1 * (Time - 1) * (Time - 1);
                    break;
                case Ease.ElasticIn:
                    t = Time <= 0
                        ? 0
                        : Time >= 1
                            ? 1
                            : -Mathf.Pow(2, 10 * Time - 10) * Mathf.Sin((Time * 10 - 10.75f) * C4);
                    break;
                case Ease.Elastic:
                    t = Time <= 0
                        ? 0
                        : Time >= 1
                            ? 1
                            : Time < 0.5f
                                ?  -(Mathf.Pow(2, 20 * Time - 10) * Mathf.Sin((20 * Time - 11.125f) * C5)) / 2
                                : (Mathf.Pow(2, -20 * Time + 10) * Mathf.Sin((20 * Time - 11.125f) * C5)) / 2 + 1;
                    break;
                case Ease.ElasticOut:
                    t = Time <= 0
                        ? 0
                        : Time >= 1
                            ? 1
                            : Mathf.Pow(2, -10 * Time) * Mathf.Sin((Time * 10 - 0.75f) * C4) + 1;
                    break;
                case Ease.BounceIn:
                {
                    var time = 1 - Time;
                    if (time < D1)
                    {
                        t = N1 * time * time;
                    }
                    else if (time < 2 * D1)
                    {
                        time -= 1.5f * D1;
                        t = N1 * time * time + 0.75f;
                    }
                    else if (time < 2.5f * D1)
                    {
                        time -= 2.25f * D1;
                        t = N1 * time * time + 0.9375f;
                    }
                    else
                    {
                        time -= 2.625f * D1;
                        t = N1 * time * time + 0.984375f;
                    }

                    t = 1 - t;

                    break;
                }
                case Ease.Bounce:
                {
                    var time = Time < 0.5f ? 1 - 2 * Time : 2 * Time - 1;
                    if (time < D1)
                    {
                        t = N1 * time * time;
                    }
                    else if (time < 2 * D1)
                    {
                        time -= 1.5f * D1;
                        t = N1 * time * time + 0.75f;
                    }
                    else if (time < 2.5f * D1)
                    {
                        time -= 2.25f * D1;
                        t = N1 * time * time + 0.9375f;
                    }
                    else
                    {
                        time -= 2.625f * D1;
                        t = N1 * time * time + 0.984375f;
                    }

                    t = Time < 0.5f 
                        ? (1 - t) * 0.5f
                        : (1 + t) * 0.5f;

                    break;
                }
                case Ease.BounceOut:
                {
                    if (Time < D1)
                    {
                        t = N1 * Time * Time;
                    }
                    else if (Time < 2 * D1)
                    {
                        var time = Time - 1.5f * D1;
                        t = N1 * time * time + 0.75f;
                    }
                    else if (Time < 2.5f * D1)
                    {
                        var time = Time - 2.25f * D1;
                        t = N1 * time * time + 0.9375f;
                    }
                    else
                    {
                        var time = Time - 2.625f * D1;
                        t = N1 * time * time + 0.984375f;
                    }

                    break;
                }
                default:
                    value = Target;
                    Velocity = default;
                    return true;
            }

            var prev = value;
            value = LinearInterpolation(Origin, Target, t);
            Velocity = GetVelocity(prev, value, deltaTime);

            return false;
        }

        protected abstract T LinearInterpolation(T a, T b, float t);
        protected abstract T GetVelocity(T a, T b, float dt);
    }
}