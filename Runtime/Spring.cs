using System;

namespace Motion
{
    [Serializable]
    public struct Spring
    {
        public float stiffness;
        public float inverseMass;
        public float damping;

        public float sqrRestSpeed;
        public float sqrRestDelta;

        public static Spring Default => Soft;

        public static Spring Soft = new Spring
        {
            stiffness = 170,
            inverseMass = 1,
            damping = 26,
            sqrRestSpeed = 0.01f * 0.01f,
            sqrRestDelta = 0.01f * 0.01f
        };

        public static Spring Fast = new Spring
        {
            stiffness = 400,
            inverseMass = 2,
            damping = 35,
            sqrRestSpeed = 0.01f * 0.01f,
            sqrRestDelta = 0.01f * 0.01f
        };

        public static Spring Bouncy = new Spring
        {
            stiffness = 100,
            inverseMass = 1,
            damping = 10,
            sqrRestSpeed = 0.01f * 0.01f,
            sqrRestDelta = 0.01f * 0.01f
        };
    }
}