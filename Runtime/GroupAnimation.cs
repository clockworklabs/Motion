using System;
using System.Collections.Generic;
using Unity.Collections;

namespace Motion
{
    public readonly struct GroupAnimationId
    {
        public readonly uint id;

        public GroupAnimationId(uint id)
        {
            this.id = id;
        }
        
        public bool HasStarted(out bool started)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
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
            if (animation is GroupAnimation)
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
            if (animation is GroupAnimation)
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
            if (animation is GroupAnimation)
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
            if (animation is GroupAnimation)
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
            if (animation is GroupAnimation)
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
            if (animation is GroupAnimation)
            {
                autoPlay = animation.AutoPlay;
                return true;
            }

            autoPlay = false;
            return false;
        }

        public GroupAnimationId OnStart(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                animation.OnStart(callback);
            }

            return this;
        }

        public GroupAnimationId OnPlay(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                animation.OnPlay(callback);
            }

            return this;
        }

        public GroupAnimationId OnPause(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                animation.OnPause(callback);
            }

            return this;
        }

        public GroupAnimationId OnStop(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                animation.OnStop(callback);
            }

            return this;
        }

        public GroupAnimationId OnComplete(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                animation.OnComplete(callback);
            }

            return this;
        }

        public bool GetDelay(out float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                delay = animation.Delay;
                return true;
            }

            delay = 0f;
            return false;
        }
        
        public GroupAnimationId SetAutoPlay(bool autoPlay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                animation.SetAutoPlay(autoPlay);
            }

            return this;
        }

        public GroupAnimationId SetDelay(float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                animation.SetDelay(delay);
            }

            return this;
        }

        public void Play()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                animation.Play();
            }
        }

        public void Pause()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                animation.Pause();
            }
        }

        public void Stop(bool complete = false)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation)
            {
                animation.Stop(complete);
            }
        }
        
        public bool GetCount(out int count)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is GroupAnimation groupAnimation)
            {
                count = groupAnimation.Count;
                return true;
            }

            count = 0;
            return false;
        }
        
        public bool Get(int index, out AnimationId animation)
        {
            if (DoMotion.GetAnimation(id) is GroupAnimation groupAnimation)
            {
                animation = groupAnimation.GetAnimation(index);
                return true;
            }

            animation = default;
            return false;
        }
        
        public static implicit operator uint(GroupAnimationId animation) => animation.id;
        public static implicit operator AnimationId(GroupAnimationId animation) => new AnimationId(animation.id);
    }
    
    public class GroupAnimation : Animation
    {
        private List<AnimationId> Animations { get; } = new List<AnimationId>();

        internal int Count => Animations.Count;

        internal override void Reset()
        {
            base.Reset();
            
            Animations.Clear();
        }

        internal override void SetAutoPlay(bool autoPlay)
        {
            base.SetAutoPlay(autoPlay);
            
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                animation.SetAutoPlay(autoPlay);
            }
        }

        internal override void SetDelay(float delay)
        {
            base.SetDelay(delay);
            
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                animation.SetDelay(animation.GetDelay() + delay);
            }
        }

        internal void Setup(AnimationId[] animations)
        {
            if (animations == null) return;
            
            for (var i = animations.Length - 1; i >= 0; i--)
            {
                var animation = animations[i];
                if(!animation.IsActive() || animation.HasStarted()) continue;
                
                animation.SetAutoPlay(false);
                
                Animations.Add(animation);
            }
        }

        protected override bool Tick(float deltaTime)
        {
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                if(!animation.IsActive())
                {
                    Animations.RemoveAtSwapBack(i);
                }
            }

            return Animations.Count == 0;
        }

        internal override void Play()
        {
            base.Play();
            
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                animation.Play();
            }
        }

        internal override void Pause()
        {
            base.Pause();
            
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                animation.Pause();
            }
        }

        protected override void OnStop(bool complete)
        {
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                animation.Stop();
            }
            
            Animations.Clear();
        }

        internal AnimationId GetAnimation(int index) => Animations[index];
    }
}