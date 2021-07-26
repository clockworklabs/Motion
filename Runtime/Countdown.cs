using System;
using UnityEngine;

namespace Motion
{
    public class Countdown : Animation
    {
        private float Accum { get; set; }
        public float Duration { get; private set; }
        
        private Action<float> OnStepCallback { get; set; }
        
        public Countdown OnStep(Action<float> callback)
        {
            OnStepCallback = callback;

            return this;
        }

        public Countdown UpdateDuration(float target)
        {
            if (!Active)
            {
                return this;
            }
            
            Duration = target;

            return this;
        }

        public Countdown UpdateDuration(RefAction<float> update)
        {
            if (!Active)
            {
                return this;
            }
            
            var target = Duration;
            update?.Invoke(ref target);
            Duration = target;

            return this;
        }
        
        internal override void Reset()
        {
            base.Reset();

            Accum = 0;
            Duration = 0;
            
            OnStepCallback = null;
        }
        
        public void Setup(float duration)
        {
            Valid = duration > 0;

            Accum = 0;
            Duration = duration;
        }

        protected override void OnStop(bool complete)
        {
            if (complete)
            {
                Accum = Duration;
            }
        }

        protected override bool Tick(float deltaTime)
        {
            Accum += deltaTime;
            OnStepCallback?.Invoke(Mathf.Max(Duration - Accum, 0));

            return Accum >= Duration;
        }
    }
}