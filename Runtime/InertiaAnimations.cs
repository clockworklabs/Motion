using UnityEngine;

namespace Motion
{
    public class FloatInertia : InertiaAnimation<float>
    {
        protected override int Dimensions => 1;
        protected override float Zero => 0f;
        protected override float MinValue => float.MinValue;
        protected override float MaxValue => float.MaxValue;

        protected override float Get(float value, int dimension) => value;

        protected override void Set(ref float value, int dimension, float component)
        {
            value = component;
        }
    }
    
    public class Vector2Inertia : InertiaAnimation<Vector2>
    {
        protected override int Dimensions => 1;
        protected override Vector2 Zero => Vector2.zero;
        protected override Vector2 MinValue => Vector2.one * float.MinValue;
        protected override Vector2 MaxValue => Vector2.one * float.MaxValue;

        protected override float Get(Vector2 value, int dimension)
        {
            switch (dimension)
            {
                case 0:
                    return value.x;
                case 1:
                    return value.y;
            }

            return 0f;
        }

        protected override void Set(ref Vector2 value, int dimension, float component)
        {
            switch (dimension)
            {
                case 0:
                    value.x = component;
                    break;
                case 1:
                    value.y = component;
                    break;
            }
        }
    }
    
    /*
    public class Vector3Spring : SpringAnimation<Vector3>
    {
        protected override Vector3 Add(Vector3 a, Vector3 b) => a + b;

        protected override Vector3 Subtract(Vector3 a, Vector3 b) => a - b;

        protected override Vector3 Multiply(Vector3 a, float b) => a * b;

        protected override float SqrMagnitude(Vector3 a) => a.sqrMagnitude;
    }
    
    public class Vector4Spring : SpringAnimation<Vector4>
    {
        protected override Vector4 Add(Vector4 a, Vector4 b) => a + b;

        protected override Vector4 Subtract(Vector4 a, Vector4 b) => a - b;

        protected override Vector4 Multiply(Vector4 a, float b) => a * b;

        protected override float SqrMagnitude(Vector4 a) => a.sqrMagnitude;
    }
    
    public class ColorSpring : SpringAnimation<Color>
    {
        protected override Color Add(Color a, Color b) => a + b;

        protected override Color Subtract(Color a, Color b) => a - b;

        protected override Color Multiply(Color a, float b) => a * b;

        protected override float SqrMagnitude(Color a) => a.r * a.r + a.g * a.g + a.b * a.b + a.a + a.a;
    }
    
    public class QuaternionSpring : SpringAnimation<Quaternion>
    {
        protected override Quaternion Add(Quaternion a, Quaternion b) => new Quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);

        protected override Quaternion Subtract(Quaternion a, Quaternion b) => new Quaternion(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);

        protected override Quaternion Multiply(Quaternion a, float b) => new Quaternion(a.x * b, a.y * b, a.z * b, a.w * b);

        protected override float SqrMagnitude(Quaternion a) => Quaternion.Dot(a, a);
    }
    */
}