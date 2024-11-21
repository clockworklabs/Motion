using System;
using UnityEngine;

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

        public static bool operator == (Tween lhs, Tween rhs) => lhs.ease == rhs.ease && 
                                                                Mathf.Approximately(lhs.duration, rhs.duration);

        public static bool operator != (Tween lhs, Tween rhs) => !(lhs == rhs);
    }
}