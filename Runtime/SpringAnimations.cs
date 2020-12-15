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
    
    public class MatrixSpring : SpringAnimation<Matrix4x4>
    {
        protected override Matrix4x4 Add(Matrix4x4 a, Matrix4x4 b) => new Matrix4x4(
            new Vector4(a.m00 + b.m00, a.m10 + b.m10, a.m20 + b.m20, a.m30 + b.m30),  
            new Vector4(a.m01 + b.m01, a.m11 + b.m11, a.m21 + b.m21, a.m31 + b.m31), 
            new Vector4(a.m02 + b.m02, a.m12 + b.m12, a.m22 + b.m22, a.m32 + b.m32), 
            new Vector4(a.m03 + b.m03, a.m13 + b.m13, a.m23 + b.m23, a.m33 + b.m33));

        protected override Matrix4x4 Subtract(Matrix4x4 a, Matrix4x4 b) => new Matrix4x4(
            new Vector4(a.m00 - b.m00, a.m10 - b.m10, a.m20 - b.m20, a.m30 - b.m30),  
            new Vector4(a.m01 - b.m01, a.m11 - b.m11, a.m21 - b.m21, a.m31 - b.m31), 
            new Vector4(a.m02 - b.m02, a.m12 - b.m12, a.m22 - b.m22, a.m32 - b.m32), 
            new Vector4(a.m03 - b.m03, a.m13 - b.m13, a.m23 - b.m23, a.m33 - b.m33));

        protected override Matrix4x4 Multiply(Matrix4x4 a, float b) => new Matrix4x4(
            new Vector4(a.m00 * b, a.m10 * b, a.m20 * b, a.m30 * b),  
            new Vector4(a.m01 * b, a.m11 * b, a.m21 * b, a.m31 * b), 
            new Vector4(a.m02 * b, a.m12 * b, a.m22 * b, a.m32 * b), 
            new Vector4(a.m03 * b, a.m13 * b, a.m23 * b, a.m33 * b));

        protected override float SqrMagnitude(Matrix4x4 a) =>
            a.m00 * a.m00 + a.m01 * a.m01 + a.m02 * a.m02 + a.m03 * a.m03 +
            a.m10 * a.m10 + a.m11 * a.m11 + a.m12 * a.m12 + a.m13 * a.m13 +
            a.m20 * a.m20 + a.m21 * a.m21 + a.m22 * a.m22 + a.m23 * a.m23 +
            a.m30 * a.m30 + a.m31 * a.m31 + a.m32 * a.m32 + a.m33 * a.m33;
    }
}