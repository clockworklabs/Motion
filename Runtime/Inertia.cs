using System;

namespace Motion
{
    [Serializable]
    public struct Inertia
    {
        public float power;

        public float restSpeed;
        public float restDelta;

        public Spring bumpSpring;

        public static Inertia Default = new Inertia
        {
            power = 0.8f,
            restSpeed = 0.01f,
            restDelta = 0.01f,
            bumpSpring = Spring.Soft
        };
    }
}