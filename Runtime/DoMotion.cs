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
        
        private HashSet<uint> ActiveIds { get; } = new HashSet<uint>();
        private List<Animation> ActiveAnimations { get; } = new List<Animation>();
        private Dictionary<Type, Stack<Animation>> FreeAnimations { get; } = new Dictionary<Type, Stack<Animation>>();
        
        private void Update()
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
            
            ActiveIds.Add(animation.ID);
            ActiveAnimations.Add(animation);
            
            return animation;
        }

        private void ReturnToPool(int index)
        {
            if (index < 0 || index >= ActiveAnimations.Count) return;
            
            var animation = ActiveAnimations[index];
            
            var type = animation.GetType();
            
            ActiveIds.Remove(animation.ID);
            ActiveAnimations.RemoveAtSwapBack(index);
            if (FreeAnimations.TryGetValue(type, out var freeStack))
            {
                freeStack.Push(animation);
            }
        }
        
        public static FloatSpring Spring(Func<float> getter, Action<float> setter,
            float target) => To<float, FloatSpring>(getter, setter, target);

        public static Vector2Spring Spring(Func<Vector2> getter, Action<Vector2> setter,
            Vector2 target) => To<Vector2, Vector2Spring>(getter, setter, target);

        public static Vector3Spring Spring(Func<Vector3> getter, Action<Vector3> setter,
            Vector3 target) => To<Vector3, Vector3Spring>(getter, setter, target);

        public static Vector4Spring Spring(Func<Vector4> getter, Action<Vector4> setter,
            Vector4 target) => To<Vector4, Vector4Spring>(getter, setter, target);

        public static ColorSpring Spring(Func<Color> getter, Action<Color> setter,
            Color target) => To<Color, ColorSpring>(getter, setter, target);

        public static QuaternionSpring Spring(Func<Quaternion> getter, Action<Quaternion> setter,
            Quaternion target) => To<Quaternion, QuaternionSpring>(getter, setter, target);
        
        public static IntTween Tween(Func<int> getter, Action<int> setter,
            int target) => To<int, IntTween>(getter, setter, target);
        
        public static FloatTween Tween(Func<float> getter, Action<float> setter,
            float target) => To<float, FloatTween>(getter, setter, target);

        public static Vector2Tween Tween(Func<Vector2> getter, Action<Vector2> setter,
            Vector2 target) => To<Vector2, Vector2Tween>(getter, setter, target);

        public static Vector3Tween Tween(Func<Vector3> getter, Action<Vector3> setter,
            Vector3 target) => To<Vector3, Vector3Tween>(getter, setter, target);

        public static Vector4Tween Tween(Func<Vector4> getter, Action<Vector4> setter,
            Vector4 target) => To<Vector4, Vector4Tween>(getter, setter, target);

        public static ColorTween Tween(Func<Color> getter, Action<Color> setter,
            Color target) => To<Color, ColorTween>(getter, setter, target);

        public static QuaternionTween Tween(Func<Quaternion> getter, Action<Quaternion> setter,
            Quaternion target) => To<Quaternion, QuaternionTween>(getter, setter, target);


        public static FloatInertia Inertia(Func<float> getter, Action<float> setter, float initialVelocity) =>
            Inertia<float, FloatInertia>(getter, setter, initialVelocity);


        public static A Inertia<T, A>(Func<T> getter, Action<T> setter, T initialVelocity) where T : struct, IEquatable<T> where A : InertiaAnimation<T>, new()
        {
            InertiaAnimation<T> animation = Instance.GetFromPool<A>();
            animation.SetInitialVelocity(initialVelocity);

            var target = animation.GetTarget(getter(), initialVelocity);
            
            Debug.Log(target);

            return To<T, A>(getter, setter, target);
        }

        public static A To<T, A>(Func<T> getter, Action<T> setter, T target) where T : struct, IEquatable<T> where A : Animation<T>, new()
        {
            var animation = Instance.GetFromPool<A>();
            animation.Setup(getter, setter, target);
            if(!animation.Valid)
            {
                Instance.ReturnToPool(Instance.ActiveAnimations.Count - 1);
            }
            
            return animation;
        }

        public static GroupAnimation Group(params Animation[] animations)
        {
            var animation = Instance.GetFromPool<GroupAnimation>();
            animation.Setup(animations);
            if(!animation.Valid)
            {
                Instance.ReturnToPool(Instance.ActiveAnimations.Count - 1);
            }
            
            return animation;
        }
    }
}