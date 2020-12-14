using System;

namespace Motion
{
    [Serializable]
    public struct Inertia
    {
        public float power;

        public float bounceStiffness;
        public float bounceInverseMass;
        public float bounceDamping;

        public float restSpeed;
        public float restDelta;

        public static Inertia Default = new Inertia
        {
            power = 0.8f,
            bounceStiffness = 170,
            bounceInverseMass = 1,
            bounceDamping = 26,
            restSpeed = 0.01f,
            restDelta = 0.01f
        };
    }
}