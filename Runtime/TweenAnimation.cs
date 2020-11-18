using System;
using UnityEngine;

namespace Motion
{
    public abstract class TweenAnimation<T> : Animation<T> where T : struct, IEquatable<T>
    {
        private const float C1 = 1.70158f;
        private const float C2 = C1 * 1.525f;
        private const float C3 = C1 + 1;
        private const float C4 =  (2 * Mathf.PI) / 3;
        private const float C5 =  (2 * Mathf.PI) / 4.5f;
        private const float N1 =  7.5625f;
        private const float D1 =  1 / 2.75f;
            
        private float inverseDuration;
        private float InverseDuration
        {
            get => inverseDuration;
            set
            {
                if (Playing) return;
                inverseDuration = value;
            }
        }

        public float Duration => 1 / InverseDuration;

        private Ease ease;
        public Ease Ease
        {
            get => ease;
            private set
            {
                if (Playing) return;
                
                ease = value;
            }
        }

        private float Time { get; set; }
        
        private bool IsInterval { get; set; }
        private float Accum { get; set; }
        
        protected override void Setup()
        {
            SetTween(Tween.Default);

            Time = 0;
            IsInterval = false;
            Accum = 0;
        }

        public TweenAnimation<T> SetOwner(object owner)
        {
            Owner = owner;

            return this;
        }

        public TweenAnimation<T> SetDelay(float delay)
        {
            Delay = delay;

            return this;
        }

        public TweenAnimation<T> SetLoops(int loops, LoopType loopType = LoopType.Restart)
        {
            LoopsCount = loops;
            LoopType = loopType;

            return this;
        }

        public TweenAnimation<T> SetInterval(int interval, float delay = 0)
        {
            Interval = interval;
            IntervalDelay = delay;

            return this;
        }

        public TweenAnimation<T> SetTween(Tween tween)
        {
            Ease = tween.ease;
            InverseDuration = 1 / tween.duration;

            return this;
        }

        public TweenAnimation<T> OnLoop(Action callback)
        {
            OnLoopCallback = callback;

            return this;
        }

        public TweenAnimation<T> OnInterval(Action callback)
        {
            OnIntervalCallback = callback;

            return this;
        }

        public TweenAnimation<T> OnComplete(Action callback)
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

            Time = Mathf.Clamp01(Time + deltaTime * InverseDuration);
            
            if (Time >= 1)
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
                        time = time - 1.5f * D1;
                        t = N1 * time * time + 0.75f;
                    }
                    else if (time < 2.5f * D1)
                    {
                        time = time - 2.25f * D1;
                        t = N1 * time * time + 0.9375f;
                    }
                    else
                    {
                        time = time - 2.625f * D1;
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
                        time = time - 1.5f * D1;
                        t = N1 * time * time + 0.75f;
                    }
                    else if (time < 2.5f * D1)
                    {
                        time = time - 2.25f * D1;
                        t = N1 * time * time + 0.9375f;
                    }
                    else
                    {
                        time = time - 2.625f * D1;
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
                    Setter(Target);
                    return true;
            }

            var value = LinearInterpolation(Origin, Target, t);
            Setter(value);
            
            return false;
        }

        protected abstract T LinearInterpolation(T a, T b, float t);
    }
}