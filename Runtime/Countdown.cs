using System;
using UnityEngine;

namespace Motion
{
    public readonly struct CountdownId
    {
        public readonly ulong id;

        public CountdownId(ulong id)
        {
            this.id = id;
        }
        
        public bool HasStarted(out bool started)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
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
            if (animation is Countdown)
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
            if (animation is Countdown)
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
            if (animation is Countdown)
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
            if (animation is Countdown)
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
            if (animation is Countdown)
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
            if (animation is Countdown)
            {
                autoPlay = animation.AutoPlay;
                return true;
            }

            autoPlay = false;
            return false;
        }

        public CountdownId OnStart(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                animation.OnStart(callback);
            }

            return this;
        }

        public CountdownId OnPlay(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                animation.OnPlay(callback);
            }

            return this;
        }

        public CountdownId OnPause(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                animation.OnPause(callback);
            }

            return this;
        }

        public CountdownId OnStop(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                animation.OnStop(callback);
            }

            return this;
        }

        public CountdownId OnComplete(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                animation.OnComplete(callback);
            }

            return this;
        }

        public bool GetDelay(out float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                delay = animation.Delay;
                return true;
            }

            delay = 0f;
            return false;
        }
        
        public CountdownId SetAutoPlay(bool autoPlay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                animation.SetAutoPlay(autoPlay);
            }

            return this;
        }

        public CountdownId SetDelay(float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                animation.SetDelay(delay);
            }

            return this;
        }

        public void Play()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                animation.Play();
            }
        }

        public void Pause()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                animation.Pause();
            }
        }

        public void Stop(bool complete = false)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown)
            {
                animation.Stop(complete);
            }
        }

        public CountdownId OnStep(Action<float> callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown countdown)
            {
                countdown.OnStep(callback);
            }

            return this;
        }

        public bool GetDuration(out float duration)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown countdown)
            {
                duration = countdown.Duration;
                return true;
            }

            duration = 0f;
            return false;
        }

        public CountdownId SetDuration(float duration)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown countdown)
            {
                countdown.SetDuration(duration);
            }

            return this;
        }

        public CountdownId SetDuration(RefAction<float> update)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Countdown countdown)
            {
                countdown.SetDuration(update);
            }

            return this;
        }
        
        public static implicit operator ulong(CountdownId animation) => animation.id;
        public static implicit operator AnimationId(CountdownId animation) => new AnimationId(animation.id);
    }
    
    public class Countdown : Animation
    {
        private float Accum { get; set; }
        public float Duration { get; private set; }
        
        private Action<float> OnStepCallback { get; set; }
        
        public void OnStep(Action<float> callback) => OnStepCallback = callback;
        
        public void SetDuration(float target)
        {
            if (!Active)
            {
                return;
            }
            
            Duration = target;
        }

        public void SetDuration(RefAction<float> update)
        {
            if (!Active)
            {
                return;
            }
            
            var target = Duration;
            update?.Invoke(ref target);
            Duration = target;
        }

        internal override void Reset()
        {
            base.Reset();

            Accum = 0;
            Duration = 0;
            
            OnStepCallback = null;
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