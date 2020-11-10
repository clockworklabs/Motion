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
            
            for (var i = Animations.Count - 1; i >= 0; i--)
            {
                var animation = Animations[i];
                DoMotion.Stop(animation.ID);
            }
            
            Animations.Clear();
        }

        internal void Setup(Animation[] animations)
        {
            if (animations == null)
            {
                return;
            }
            
            for (var i = animations.Length - 1; i >= 0; i--)
            {
                var animation = animations[i];
                if(animation.Playing) continue;
                
                animation.ManualStep = true;
                
                Animations.Add(animation);
            }
        }

        protected override bool Tick(float deltaTime)
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
            
            return Animations.Count == 0;
        }
    }
}