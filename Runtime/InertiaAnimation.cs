using System;
using UnityEngine;

namespace Motion
{
    public class InertiaAnimation : Animation<float>
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

                min = Mathf.Clamp(value, float.MinValue, max);
            }
        }

        private float max;
        public float Max
        {
            get => max;
            private set
            {
                if (Started) return;

                max = Mathf.Clamp(value, min, float.MaxValue);
            }
        }

        private float minLimit;
        public float MinLimit
        {
            get => minLimit;
            private set
            {
                if (Started) return;

                minLimit = Mathf.Min(Min, value);
            }
        }

        private float maxLimit;
        public float MaxLimit
        {
            get => maxLimit;
            private set
            {
                if (Started) return;

                maxLimit = Mathf.Max(Max, value);
            }
        }
        
        private SpringAnimation<float> SpringAnimation { get; set; }
        
        private bool IsInterval { get; set; }
        private float Accum { get; set; }

        protected override bool Check()
        {
            return !Mathf.Approximately(Origin, Target) || Origin < Min || Origin > Min;
        }

        protected override void Setup()
        {
            SetInertia(Inertia.Default);

            InitialVelocity = 0;
            Min = float.MinValue;
            Max = float.MaxValue;
            MinLimit = float.MinValue;
            MaxLimit = float.MaxValue;

            SpringAnimation = null;
            IsInterval = false;
            Accum = 0;
        }

        public InertiaAnimation SetInertia(Inertia inertia)
        {
            Inertia = inertia;

            return this;
        }

        public InertiaAnimation SetInitialVelocity(float velocity)
        {
            InitialVelocity = velocity;

            return this;
        }

        public InertiaAnimation SetMinBoundary(float boundary, float limit = float.MinValue)
        {
            Min = boundary;
            MinLimit = limit;

            return this;
        }

        public InertiaAnimation SetMaxBoundary(float boundary, float limit = float.MaxValue)
        {
            Max = boundary;
            MaxLimit = limit;

            return this;
        }

        public InertiaAnimation SetBoundaries(float min, float max)
        {
            if(min > max) throw new UnityException("min must be smaller than max");
            
            SetMinBoundary(min);
            SetMaxBoundary(max);

            return this;
        }

        public InertiaAnimation SetBoundaries(float min, float max, float limitRatio)
        {
            if(min > max) throw new UnityException("min must be smaller than max");
            
            var length = max - min;
            var limitSize = length * limitRatio;
            SetMinBoundary(min, min - limitSize);
            SetMaxBoundary(max, max + limitSize);

            return this;
        }

        public InertiaAnimation SetBoundaries(float min, float max, float minLimit, float maxLimit)
        {
            if(min > max) throw new UnityException("min must be smaller than max");
            
            SetMinBoundary(min, minLimit);
            SetMaxBoundary(max, maxLimit);

            return this;
        }
        
        public override void Play()
        {
            base.Play();

            SpringAnimation?.Play();
        }

        public override void Pause()
        {
            base.Pause();

            SpringAnimation?.Pause();
        }

        protected override void OnStop(bool complete)
        {
            SpringAnimation?.Stop(Owner);
            
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

            if (SpringAnimation != null)
            {
                return new TickResult
                {
                    complete = !SpringAnimation.Active
                };
            }
            
            var value = Getter();
            var delta = value - Target;
            if (value >= Min && value <= Max && Mathf.Abs(InitialVelocity) < Inertia.restSpeed && Mathf.Abs(delta) < Inertia.restDelta)
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
            
            var t = Mathf.InverseLerp(Target, Origin, value);
            var velocity = InitialVelocity * Mathf.Pow(t, Inertia.power);
            
            float multiplier;
            if (velocity < 0 && value < Min && MinLimit > float.MinValue)
            {
                var mt = 1 - Mathf.InverseLerp(MinLimit, Min, value);
                multiplier = Mathf.Clamp01(1 - mt * mt);
            } else if (velocity > 0 && value > Max && MaxLimit < float.MaxValue)
            {
                var mt = Mathf.InverseLerp(Max, MaxLimit, value);
                multiplier = Mathf.Clamp01(1 - mt * mt);
            }
            else
            {
                multiplier = 1f;
            }

            velocity *= multiplier;

            value = Mathf.Clamp(value + velocity * deltaTime, MinLimit, MaxLimit);

            Setter(value);

            if (value < Min)
            {
                SpringAnimation = DoMotion.Spring(Getter, Setter, Min).SetSpring(Inertia.bumpSpring).SetInitialVelocity(velocity);
                SpringAnimation.SetOwner(Owner).Play();
            } else if (value > Max)
            {
                SpringAnimation = DoMotion.Spring(Getter, Setter, Max).SetSpring(Inertia.bumpSpring).SetInitialVelocity(velocity);
                SpringAnimation.SetOwner(Owner).Play();
            }
            
            return new TickResult();
        }
    }
}