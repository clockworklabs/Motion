using System;
using UnityEngine;

namespace Motion
{
    [Serializable]
    public struct Tween : IEquatable<Tween>
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

        public bool Equals(Tween other) => ease == other.ease && Mathf.Approximately(duration, other.duration);
    }
}