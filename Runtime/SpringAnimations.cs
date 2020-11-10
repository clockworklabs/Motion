using UnityEngine;

namespace Motion
{
    public class FloatSpring : SpringAnimation<float>
    {
        protected override float Add(float a, float b) => a + b;

        protected override float Subtract(float a, float b) => a - b;

        protected override float Multiply(float a, float b) => a * b;

        protected override float SqrMagnitude(float a) => a * a;
    }
    
    public class Vector2Spring : SpringAnimation<Vector2>
    {
        protected override Vector2 Add(Vector2 a, Vector2 b) => a + b;

        protected override Vector2 Subtract(Vector2 a, Vector2 b) => a - b;

        protected override Vector2 Multiply(Vector2 a, float b) => a * b;

        protected override float SqrMagnitude(Vector2 a) => a.sqrMagnitude;
    }
    
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
}