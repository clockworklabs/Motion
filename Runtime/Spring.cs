using System;
using UnityEngine;

namespace Motion
{
    [Serializable]
    public struct Spring : IEquatable<Spring>
    {
        public float inverseMass;
        public float stiffness;
        public float damping;

        public float sqrRestSpeed;
        public float sqrRestDelta;

        public float mass
        {
            get => 1 / inverseMass;
            set => inverseMass = 1 / value;
        }
        public float restSpeed
        {
            get => Mathf.Sqrt(sqrRestSpeed);
            set => sqrRestSpeed = value * value;
        }
        public float restDelta
        {
            get => Mathf.Sqrt(sqrRestDelta);
            set => sqrRestDelta = value * value;
        }

        public static Spring Default => Soft;

        public Spring(float mass, float stiffness, float damping, float restSpeed = 0.05f, float restDelta = 0.05f)
        {
            inverseMass = 1 / mass;
            this.stiffness = stiffness;
            this.damping = damping;
            sqrRestSpeed = restSpeed * restSpeed;
            sqrRestDelta = restDelta * restDelta;           
        }

        public Spring(Spring other)
        {
            stiffness = other.stiffness;
            inverseMass = other.inverseMass;
            damping = other.damping;
            sqrRestSpeed = other.sqrRestSpeed;
            sqrRestDelta = other.sqrRestDelta;
        }

        public static Spring Soft = new Spring
        {
            stiffness = 170,
            inverseMass = 1,
            damping = 26,
            sqrRestSpeed = 0.05f * 0.05f,
            sqrRestDelta = 0.05f * 0.05f
        };

        public static Spring Fast = new Spring
        {
            stiffness = 400,
            inverseMass = 2,
            damping = 35,
            sqrRestSpeed = 0.05f * 0.05f,
            sqrRestDelta = 0.05f * 0.05f
        };

        public static Spring Bouncy = new Spring
        {
            stiffness = 100,
            inverseMass = 1,
            damping = 10,
            sqrRestSpeed = 0.05f * 0.05f,
            sqrRestDelta = 0.05f * 0.05f
        };

        public static bool operator ==(Spring lhs, Spring rhs) => lhs.Equals(rhs);

        public static bool operator !=(Spring lhs, Spring rhs) => !(lhs == rhs);

        public bool Equals(Spring other) => Mathf.Approximately(this.stiffness, other.stiffness) && 
                                            Mathf.Approximately(this.inverseMass, other.inverseMass) && 
                                            Mathf.Approximately(this.damping, other.damping) && 
                                            Mathf.Approximately(this.sqrRestSpeed, other.sqrRestSpeed) && 
                                            Mathf.Approximately(this.sqrRestDelta, other.sqrRestDelta);
        public override bool Equals(object obj) => obj is Spring other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(inverseMass, stiffness, damping, sqrRestSpeed, sqrRestDelta);
    }
}