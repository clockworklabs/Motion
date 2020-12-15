using System;

namespace Motion
{    
    
    [Serializable]
    public struct Tween
    {
        public Ease ease;
        public float duration;

        public Tween(Tween other)
        {
            ease = other.ease;
            duration = other.duration;
        }

        public static Tween Default = new Tween
        {
            ease = Ease.Back,
            duration = 1
        };
    }
}