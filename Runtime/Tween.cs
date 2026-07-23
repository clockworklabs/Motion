using System;
using UnityEngine;

namespace Motion
{
    [Serializable]
    public struct Tween : IEquatable<Tween>
    {
        public Ease ease;
        public float duration;

        public Tween(Ease ease, float duration)
        {
            this.ease = ease;
            this.duration = duration;
        }

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

        public static bool operator == (Tween lhs, Tween rhs) => lhs.Equals(rhs);

        public static bool operator != (Tween lhs, Tween rhs) => !(lhs == rhs);
        
        public bool Equals(Tween other) => this.ease == other.ease && Mathf.Approximately(this.duration, other.duration);
        public override bool Equals(object obj) => obj is Tween other && Equals(other);
        public override int GetHashCode() => HashCode.Combine((int)ease, duration);
    }
}