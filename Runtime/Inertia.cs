using System;
using UnityEngine;

namespace Motion
{
    [Serializable]
    public struct Inertia : IEquatable<Inertia>
    {
        public float power;

        public float bounceStiffness;
        public float bounceInverseMass;
        public float bounceDamping;

        public float restSpeed;
        public float restDelta;

        public Inertia(Inertia other)
        {
            power = other.power;
            bounceStiffness = other.bounceStiffness;
            bounceInverseMass = other.bounceInverseMass;
            bounceDamping = other.bounceDamping;
            restSpeed = other.restSpeed;
            restDelta = other.restDelta;
        }

        public static Inertia Default = new Inertia
        {
            power = 0.8f,
            bounceStiffness = 170,
            bounceInverseMass = 1,
            bounceDamping = 26,
            restSpeed = 0.05f,
            restDelta = 0.05f
        };

        public bool Equals(Inertia other)
        {
            if (!Mathf.Approximately(power, other.power))
            {
                return false;
            }
            if (!Mathf.Approximately(bounceStiffness, other.bounceStiffness))
            {
                return false;
            }
            if (!Mathf.Approximately(bounceInverseMass, other.bounceInverseMass))
            {
                return false;
            }
            if (!Mathf.Approximately(bounceDamping, other.bounceDamping))
            {
                return false;
            }
            if (!Mathf.Approximately(restSpeed, other.restSpeed))
            {
                return false;
            }
            if (!Mathf.Approximately(restDelta, other.restDelta))
            {
                return false;
            }

            return true;
        }
    }
}