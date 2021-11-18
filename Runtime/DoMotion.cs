using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Motion
{
    public class DoMotion : MonoBehaviour
    {
        #region Singleton
        private static DoMotion instance;
        private static DoMotion Instance
        {
            get
            {
                if (instance == null)
                {
                    var gameObject = new GameObject("Motion", typeof(DoMotion));
                    instance = gameObject.GetComponent<DoMotion>();
                }

                return instance;
            }
        }

        private void Start()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(this);
        }
        #endregion
        
        private List<Animation> ActiveAnimations { get; } = new List<Animation>();
        private Dictionary<uint, Animation> ActiveAnimationsById { get; } = new Dictionary<uint, Animation>();
        private Dictionary<Type, Stack<Animation>> FreeAnimations { get; } = new Dictionary<Type, Stack<Animation>>();
        
        private static uint _nextId;
        
        private void LateUpdate()
        {
            for (var i = ActiveAnimations.Count - 1; i >= 0; i--)
            {
                var animation = ActiveAnimations[i];

                if (!animation.Started && animation.AutoPlay)
                {
                    animation.Play();
                }
                
                animation.Step(Time.deltaTime);

                if (!animation.Active)
                {
                    ReturnToPool(i);
                }
            }
        }

        private T GetFromPool<T>() where T : Animation, new()
        {
            var type = typeof(T);
            if (!FreeAnimations.TryGetValue(type, out var freeStack))
            {
                freeStack = new Stack<Animation>();
                FreeAnimations[type] = freeStack;
            }

            T animation;
            if (freeStack.Count > 0)
            {
                animation = (T) freeStack.Pop();
                animation.Reset();
            }
            else
            {
                animation = new T();
                animation.Reset();
            }

            var id = ++_nextId;

            animation.Id = id;
            
            ActiveAnimations.Add(animation);
            ActiveAnimationsById.Add(id, animation);
            
            return animation;
        }

        private void ReturnToPool(int index)
        {
            if (index < 0 || index >= ActiveAnimations.Count) return;
            
            var animation = ActiveAnimations[index];
            
            var type = animation.GetType();
            
            ActiveAnimations.RemoveAtSwapBack(index);
            ActiveAnimationsById.Remove(animation.Id);
            if (FreeAnimations.TryGetValue(type, out var freeStack))
            {
                freeStack.Push(animation);
            }
        }
        
        public static SpringAnimationId<T> Spring<T, A>(Func<T> getter, Action<T> setter, T target) where T : struct, IEquatable<T> where A : SpringAnimation<T>, new() => Spring<T, A>(getter, setter, target, Motion.Spring.Default);
        public static SpringAnimationId<T> Spring<T, A>(Func<T> getter, Action<T> setter, T target, Spring spring) where T : struct, IEquatable<T> where A : SpringAnimation<T>, new()
        {
            var animation = Instance.GetFromPool<A>();
            animation.Setup(getter, setter, target);
            animation.SetSpring(spring);
            
            return new SpringAnimationId<T>(animation.Id);
        }
        
        public static TweenAnimationId<T> Tween<T, A>(Func<T> getter, Action<T> setter, T target, float duration = 1, Ease ease = Ease.Back) where T : struct, IEquatable<T> where A : TweenAnimation<T>, new() =>
            Tween<T, A>(getter, setter, target, new Tween
            {
                duration = duration,
                ease = ease
            });
        public static TweenAnimationId<T> Tween<T, A>(Func<T> getter, Action<T> setter, T target, Tween tween) where T : struct, IEquatable<T> where A : TweenAnimation<T>, new()
        {
            var animation = Instance.GetFromPool<A>();
            animation.Setup(getter, setter, target);
            animation.SetTween(tween);
            
            return new TweenAnimationId<T>(animation.Id);
        }

        public static InertiaAnimationId Inertia(Func<float> getter, Action<float> setter, float velocity) =>
            Inertia(getter, setter, velocity, Motion.Inertia.Default);
        public static InertiaAnimationId Inertia(Func<float> getter, Action<float> setter, float velocity, float min, float max) =>
            Inertia(getter, setter, velocity, Motion.Inertia.Default, min, max);
        public static InertiaAnimationId Inertia(Func<float> getter, Action<float> setter, float velocity, Inertia inertia) =>
            Inertia(getter, setter, velocity, inertia, float.MinValue, float.MaxValue);
        public static InertiaAnimationId Inertia(Func<float> getter, Action<float> setter, float velocity, Inertia inertia, float min, float max)
        {
            var animation = Instance.GetFromPool<InertiaAnimation>();
            animation.Setup(getter, setter);
            animation.SetInertia(inertia);
            animation.SetInitialVelocity(velocity);
            animation.SetBoundaries(min, max);
            
            return new InertiaAnimationId(animation.Id);
        }
        
        public static GroupAnimationId Group(params AnimationId[] animations)
        {
            var animation = Instance.GetFromPool<GroupAnimation>();
            animation.Setup(animations);
            
            return new GroupAnimationId(animation.Id);
        }

        public static SpringAnimationId<float> Spring(Func<float> getter, Action<float> setter, float target) =>
            Spring(getter, setter, target, Motion.Spring.Default);
        public static SpringAnimationId<float> Spring(Func<float> getter, Action<float> setter, float target, Spring spring) =>
            Spring<float, FloatSpring>(getter, setter, target, spring);

        public static SpringAnimationId<Vector2> Spring(Func<Vector2> getter, Action<Vector2> setter, Vector2 target) =>
            Spring(getter, setter, target, Motion.Spring.Default);
        public static SpringAnimationId<Vector2> Spring(Func<Vector2> getter, Action<Vector2> setter, Vector2 target, Spring spring) =>
            Spring<Vector2, Vector2Spring>(getter, setter, target, spring);

        public static SpringAnimationId<Vector3> Spring(Func<Vector3> getter, Action<Vector3> setter, Vector3 target) =>
            Spring(getter, setter, target, Motion.Spring.Default);
        public static SpringAnimationId<Vector3> Spring(Func<Vector3> getter, Action<Vector3> setter, Vector3 target, Spring spring) =>
            Spring<Vector3, Vector3Spring>(getter, setter, target, spring);

        public static SpringAnimationId<Vector4> Spring(Func<Vector4> getter, Action<Vector4> setter, Vector4 target) =>
            Spring(getter, setter, target, Motion.Spring.Default);
        public static SpringAnimationId<Vector4> Spring(Func<Vector4> getter, Action<Vector4> setter, Vector4 target, Spring spring) =>
            Spring<Vector4, Vector4Spring>(getter, setter, target, spring);

        public static SpringAnimationId<Color> Spring(Func<Color> getter, Action<Color> setter, Color target) =>
            Spring(getter, setter, target, Motion.Spring.Default);
        public static SpringAnimationId<Color> Spring(Func<Color> getter, Action<Color> setter, Color target, Spring spring) =>
            Spring<Color, ColorSpring>(getter, setter, target, spring);

        public static SpringAnimationId<Quaternion> Spring(Func<Quaternion> getter, Action<Quaternion> setter, Quaternion target) =>
            Spring(getter, setter, target, Motion.Spring.Default);
        public static SpringAnimationId<Quaternion> Spring(Func<Quaternion> getter, Action<Quaternion> setter, Quaternion target, Spring spring) =>
            Spring<Quaternion, QuaternionSpring>(getter, setter, target, spring);

        public static SpringAnimationId<Matrix4x4> Spring(Func<Matrix4x4> getter, Action<Matrix4x4> setter, Matrix4x4 target) =>
            Spring(getter, setter, target, Motion.Spring.Default); 
        public static SpringAnimationId<Matrix4x4> Spring(Func<Matrix4x4> getter, Action<Matrix4x4> setter, Matrix4x4 target, Spring spring) => 
            Spring<Matrix4x4, MatrixSpring>(getter, setter, target, spring);

        public static TweenAnimationId<int> Tween(Func<int> getter, Action<int> setter, int target, float duration = 1, Ease ease = Ease.Back) =>
            Tween(getter, setter, target, new Tween
            {
                duration = duration,
                ease = ease
            });
        public static TweenAnimationId<int> Tween(Func<int> getter, Action<int> setter, int target, Tween tween) => 
            Tween<int, IntTween>(getter, setter, target, tween);
        
        public static TweenAnimationId<float> Tween(Func<float> getter, Action<float> setter, float target, float duration = 1, Ease ease = Ease.Back) =>
            Tween(getter, setter, target, new Tween
            {
                duration = duration,
                ease = ease
            });
        public static TweenAnimationId<float> Tween(Func<float> getter, Action<float> setter, float target, Tween tween) =>
            Tween<float, FloatTween>(getter, setter, target, tween);

        public static TweenAnimationId<Vector2> Tween(Func<Vector2> getter, Action<Vector2> setter, Vector2 target, float duration = 1, Ease ease = Ease.Back) =>
            Tween(getter, setter, target, new Tween
            {
                duration = duration,
                ease = ease
            }); 
        public static TweenAnimationId<Vector2> Tween(Func<Vector2> getter, Action<Vector2> setter, Vector2 target, Tween tween) => 
            Tween<Vector2, Vector2Tween>(getter, setter, target, tween);

        public static TweenAnimationId<Vector3> Tween(Func<Vector3> getter, Action<Vector3> setter, Vector3 target, float duration = 1, Ease ease = Ease.Back) =>
            Tween(getter, setter, target, new Tween
            {
                duration = duration,
                ease = ease
            });
        public static TweenAnimationId<Vector3> Tween(Func<Vector3> getter, Action<Vector3> setter, Vector3 target, Tween tween) =>
            Tween<Vector3, Vector3Tween>(getter, setter, target, tween);

        public static TweenAnimationId<Vector4> Tween(Func<Vector4> getter, Action<Vector4> setter, Vector4 target, float duration = 1, Ease ease = Ease.Back) =>
            Tween(getter, setter, target, new Tween
            {
                duration = duration,
                ease = ease
            });
        public static TweenAnimationId<Vector4> Tween(Func<Vector4> getter, Action<Vector4> setter, Vector4 target, Tween tween) => 
            Tween<Vector4, Vector4Tween>(getter, setter, target, tween);

        public static TweenAnimationId<Color> Tween(Func<Color> getter, Action<Color> setter, Color target, float duration = 1, Ease ease = Ease.Back) =>
            Tween(getter, setter, target, new Tween
            {
                duration = duration,
                ease = ease
            });
        public static TweenAnimationId<Color> Tween(Func<Color> getter, Action<Color> setter, Color target, Tween tween) => 
            Tween<Color, ColorTween>(getter, setter, target, tween);

        public static TweenAnimationId<Quaternion> Tween(Func<Quaternion> getter, Action<Quaternion> setter, Quaternion target, float duration = 1, Ease ease = Ease.Back) =>
            Tween(getter, setter, target, new Tween
            {
                duration = duration,
                ease = ease
            });
        public static TweenAnimationId<Quaternion> Tween(Func<Quaternion> getter, Action<Quaternion> setter, Quaternion target, Tween tween) => 
            Tween<Quaternion, QuaternionTween>(getter, setter, target, tween);

        public static TweenAnimationId<Matrix4x4> Tween(Func<Matrix4x4> getter, Action<Matrix4x4> setter, Matrix4x4 target, float duration = 1, Ease ease = Ease.Back) =>
            Tween(getter, setter, target, new Tween
            {
                duration = duration,
                ease = ease
            });
        public static TweenAnimationId<Matrix4x4> Tween(Func<Matrix4x4> getter, Action<Matrix4x4> setter, Matrix4x4 target, Tween tween) =>
            Tween<Matrix4x4, MatrixTween>(getter, setter, target, tween);

        public static CountdownId Countdown(float time, Action callback)
        {
            var animation = Instance.GetFromPool<Countdown>();
            animation.SetDuration(time);
            animation.OnComplete(callback);
            
            return new CountdownId(animation.Id);
        }

        internal static Animation GetAnimation(uint id)
        {
            return Instance.ActiveAnimationsById.TryGetValue(id, out var animation) ? animation : null;
        }
    }
}