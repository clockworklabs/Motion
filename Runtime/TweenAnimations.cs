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
        protected override Quaternion LinearInterpolation(Quaternion a, Quaternion b, float t) => Quaternion.LerpUnclamped(a, b, t);
    }
}