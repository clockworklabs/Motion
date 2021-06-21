using System;
using UnityEngine;

namespace Motion
{
    [Serializable]
    public struct Spring : IEquatable<Spring>
    {
        public float stiffness;
        public float inverseMass;
        public float damping;

        public float sqrRestSpeed;
        public float sqrRestDelta;

        public static Spring Default => Soft;

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

        public bool Equals(Spring other)
        {
            if (!Mathf.Approximately(stiffness, other.stiffness))
            {
                return false;
            }
            if (!Mathf.Approximately(inverseMass, other.inverseMass))
            {
                return false;
            }
            if (!Mathf.Approximately(damping, other.damping))
            {
                return false;
            }
            if (!Mathf.Approximately(sqrRestSpeed, other.sqrRestSpeed))
            {
                return false;
            }
            if (!Mathf.Approximately(sqrRestDelta, other.sqrRestDelta))
            {
                return false;
            }

            return true;
        }
    }
}