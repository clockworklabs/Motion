using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Motion.Utils
{
    public static class MatrixUtils
    {
        public static Matrix4x4 Add(Matrix4x4 a, Matrix4x4 b) => new Matrix4x4(
            new Vector4(a.m00 + b.m00, a.m10 + b.m10, a.m20 + b.m20, a.m30 + b.m30),  
            new Vector4(a.m01 + b.m01, a.m11 + b.m11, a.m21 + b.m21, a.m31 + b.m31), 
            new Vector4(a.m02 + b.m02, a.m12 + b.m12, a.m22 + b.m22, a.m32 + b.m32), 
            new Vector4(a.m03 + b.m03, a.m13 + b.m13, a.m23 + b.m23, a.m33 + b.m33));

        public static Matrix4x4 Subtract(Matrix4x4 a, Matrix4x4 b) => new Matrix4x4(
            new Vector4(a.m00 - b.m00, a.m10 - b.m10, a.m20 - b.m20, a.m30 - b.m30),  
            new Vector4(a.m01 - b.m01, a.m11 - b.m11, a.m21 - b.m21, a.m31 - b.m31), 
            new Vector4(a.m02 - b.m02, a.m12 - b.m12, a.m22 - b.m22, a.m32 - b.m32), 
            new Vector4(a.m03 - b.m03, a.m13 - b.m13, a.m23 - b.m23, a.m33 - b.m33));

        public static Matrix4x4 Multiply(Matrix4x4 a, float b) => new Matrix4x4(
            new Vector4(a.m00 * b, a.m10 * b, a.m20 * b, a.m30 * b),  
            new Vector4(a.m01 * b, a.m11 * b, a.m21 * b, a.m31 * b), 
            new Vector4(a.m02 * b, a.m12 * b, a.m22 * b, a.m32 * b), 
            new Vector4(a.m03 * b, a.m13 * b, a.m23 * b, a.m33 * b));
        
        public static Matrix4x4 Lerp(Matrix4x4 a, Matrix4x4 b, float t) => new Matrix4x4(
            new Vector4(Mathf.LerpUnclamped(a.m00, b.m00, t), Mathf.LerpUnclamped(a.m10, b.m10, t), Mathf.LerpUnclamped(a.m20, b.m20, t), Mathf.LerpUnclamped(a.m30, b.m30, t)),  
            new Vector4(Mathf.LerpUnclamped(a.m01, b.m01, t), Mathf.LerpUnclamped(a.m11, b.m11, t), Mathf.LerpUnclamped(a.m21, b.m21, t), Mathf.LerpUnclamped(a.m31, b.m31, t)), 
            new Vector4(Mathf.LerpUnclamped(a.m02, b.m02, t), Mathf.LerpUnclamped(a.m12, b.m12, t), Mathf.LerpUnclamped(a.m22, b.m22, t), Mathf.LerpUnclamped(a.m32, b.m32, t)), 
            new Vector4(Mathf.LerpUnclamped(a.m03, b.m03, t), Mathf.LerpUnclamped(a.m13, b.m13, t), Mathf.LerpUnclamped(a.m23, b.m23, t), Mathf.LerpUnclamped(a.m33, b.m33, t)));
    }
}