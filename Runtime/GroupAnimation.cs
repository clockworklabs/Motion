using System;
using System.Collections.Generic;
using Unity.Collections;

namespace Motion
{
    public class GroupAnimation : Animation
    {
        private List<Animation> Animations { get; } = new List<Animation>();

        internal override void Reset()
        {
            base.Reset();
            
            Animations.Clear();
        }

        public override Animation SetAutoPlay(bool autoPlay)
        {
            base.SetAutoPlay(autoPlay);
            
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                animation.SetAutoPlay(autoPlay);
                
            }

            return this;
        }

        public override Animation SetOwner(object owner)
        {
            base.SetOwner(owner);
            
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                animation.SetOwner(owner);
                
            }

            return this;
        }

        public override Animation SetDelay(float delay)
        {
            base.SetDelay(delay);
            
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                animation.SetDelay(animation.Delay + delay);
            }

            return this;
        }

        internal void Setup(Animation[] animations)
        {
            if (animations == null) return;
            
            for (var i = animations.Length - 1; i >= 0; i--)
            {
                var animation = animations[i];
                if(!animation.Active || animation.Started) continue;
                
                animation.SetAutoPlay(false);
                animation.SetOwner(null);
                
                Animations.Add(animation);
            }

            Valid = Animations.Count > 0;
        }

        protected override TickResult Tick(float deltaTime)
        {
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                if(!animation.Active)
                {
                    Animations.RemoveAtSwapBack(i);
                }
            }

            return new TickResult
            {
                complete = Animations.Count == 0
            };
        }

        public override void Play()
        {
            base.Play();
            
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                animation.Play();
            }
        }

        public override void Pause()
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
                animation.Stop(complete, Owner);
            }
            
            Animations.Clear();
        }
    }
}