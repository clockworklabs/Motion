using UnityEngine;

namespace Motion
{
    public class IntTween : TweenAnimation<int>
    {
        protected override int LinearInterpolation(int a, int b, float t) => Mathf.RoundToInt(Mathf.LerpUnclamped(a, b, t));
    }
    
    public class FloatTween : TweenAnimation<float>
    {
        protected override float LinearInterpolation(float a, float b, float t) => Mathf.LerpUnclamped(a, b, t);
    }
    
    public class Vector2Tween : TweenAnimation<Vector2>
    {
        protected override Vector2 LinearInterpolation(Vector2 a, Vector2 b, float t) => Vector2.LerpUnclamped(a, b, t);
    }
    
    public class Vector3Tween : TweenAnimation<Vector3>
    {
        protected override Vector3 LinearInterpolation(Vector3 a, Vector3 b, float t) => Vector3.LerpUnclamped(a, b, t);
    }
    
    public class Vector4Tween : TweenAnimation<Vector4>
    {
        protected override Vector4 LinearInterpolation(Vector4 a, Vector4 b, float t) => Vector4.LerpUnclamped(a, b, t);
    }
    
    public class ColorTween : TweenAnimation<Color>
    {
        protected override Color LinearInterpolation(Color a, Color b, float t) => Color.LerpUnclamped(a, b, t);
    }
    
    public class QuaternionTween : TweenAnimation<Quaternion>
    {

        private bool slerp;
        public bool Slerp
        {
            get => slerp;
            private set
            {
                if (Started) return;
                
                slerp = value;
            }
        }

        public QuaternionTween SetSlerp(bool slerp)
        {
            Slerp = slerp;
            
            return this;
        }

        internal override void Reset()
        {
            base.Reset();
            
            SetSlerp(true);
        }
        
        protected override Quaternion LinearInterpolation(Quaternion a, Quaternion b, float t) => Quaternion.SlerpUnclamped(a, b, t);
    }
    
    public class MatrixTween : TweenAnimation<Matrix4x4>
    {
        protected override Matrix4x4 LinearInterpolation(Matrix4x4 a, Matrix4x4 b, float t) => new Matrix4x4(
            new Vector4(Mathf.LerpUnclamped(a.m00, b.m00, t), Mathf.LerpUnclamped(a.m10, b.m10, t), Mathf.LerpUnclamped(a.m20, b.m20, t), Mathf.LerpUnclamped(a.m30, b.m30, t)),  
            new Vector4(Mathf.LerpUnclamped(a.m01, b.m01, t), Mathf.LerpUnclamped(a.m11, b.m11, t), Mathf.LerpUnclamped(a.m21, b.m21, t), Mathf.LerpUnclamped(a.m31, b.m31, t)), 
            new Vector4(Mathf.LerpUnclamped(a.m02, b.m02, t), Mathf.LerpUnclamped(a.m12, b.m12, t), Mathf.LerpUnclamped(a.m22, b.m22, t), Mathf.LerpUnclamped(a.m32, b.m32, t)), 
            new Vector4(Mathf.LerpUnclamped(a.m03, b.m03, t), Mathf.LerpUnclamped(a.m13, b.m13, t), Mathf.LerpUnclamped(a.m23, b.m23, t), Mathf.LerpUnclamped(a.m33, b.m33, t)));
    }
}