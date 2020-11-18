using System;
using System.Collections.Generic;
using Unity.Collections;

namespace Motion
{
    public class GroupAnimation : Animation
    {
        private List<Animation> Animations { get; } = new List<Animation>();

        public GroupAnimation SetOwner(object owner)
        {
            Owner = owner;
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                animation.Owner = owner;
            }

            return this;
        }

        public GroupAnimation OnComplete(Action callback)
        {
            OnCompleteCallback = callback;
            
            return this;
        }

        internal override void Reset()
        {
            base.Reset();
            
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                DoMotion.Stop(animation.ID, Owner);
            }
            
            Animations.Clear();
        }

        internal void Setup(Animation[] animations)
        {
            if (animations == null) return;
            
            for (var i = animations.Length - 1; i >= 0; i--)
            {
                var animation = animations[i];
                if(animation.Playing) continue;
                
                animation.ManualStep = true;
                
                Animations.Add(animation);
            }
        }

        protected override TickResult Tick(float deltaTime)
        {
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                
                animation.Step(deltaTime);
                
                if (animation.Completed)
                {
                    Animations.RemoveAtSwapBack(i);
                }
            }

            return new TickResult
            {
                complete = Animations.Count == 0
            };
        }
    }
}