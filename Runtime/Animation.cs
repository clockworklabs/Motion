using System;
using UnityEngine;

namespace Motion
{
    public delegate void RefAction<T>(ref T value) where T : struct;
    
    public readonly struct AnimationId
    {
        public readonly ulong id;

        public AnimationId(ulong id)
        {
            this.id = id;
        }

        public T As<T>() where T : Animation => DoMotion.GetAnimation(id) as T;
        public bool Is<T>() where T : Animation => As<T>() != null;
        public bool Is<T>(out T animation) where T : Animation
        {
            animation = As<T>();
            return animation != null;
        }
        
        public bool HasStarted()
        {
            var animation = DoMotion.GetAnimation(id);
            return animation?.Started ?? false;
        }
        public bool IsPlaying()
        {
            var animation = DoMotion.GetAnimation(id);
            return animation?.Playing ?? false;
        }
        public bool IsStopped()
        {
            var animation = DoMotion.GetAnimation(id);
            return animation?.Stopped ?? false;
        }
        public bool IsCompleted()
        {
            var animation = DoMotion.GetAnimation(id);
            return animation?.Completed ?? false;
        }
        public bool IsActive()
        {
            var animation = DoMotion.GetAnimation(id);
            return animation?.Active ?? false;
        }

        public bool IsPaused()
        {
            var animation = DoMotion.GetAnimation(id);
            return animation?.Paused ?? false;
        }

        public bool IsAutoPlay()
        {
            var animation = DoMotion.GetAnimation(id);
            return animation?.AutoPlay ?? false;
        }

        public AnimationId OnStart(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            animation?.OnStart(callback);

            return this;
        }

        public AnimationId OnPlay(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            animation?.OnPlay(callback);

            return this;
        }

        public AnimationId OnPause(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            animation?.OnPause(callback);

            return this;
        }

        public AnimationId OnStop(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            animation?.OnStop(callback);

            return this;
        }

        public AnimationId OnComplete(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            animation?.OnComplete(callback);

            return this;
        }

        public float GetDelay()
        {
            var animation = DoMotion.GetAnimation(id);
            return animation?.Delay ?? 0f;
        }
        
        public AnimationId SetAutoPlay(bool autoPlay)
        {
            var animation = DoMotion.GetAnimation(id);
            animation?.SetAutoPlay(autoPlay);

            return this;
        }

        public AnimationId SetDelay(float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            animation?.SetDelay(delay);

            return this;
        }

        public void Play()
        {
            var animation = DoMotion.GetAnimation(id);
            animation?.Play();
        }

        public void Pause()
        {
            var animation = DoMotion.GetAnimation(id);
            animation?.Pause();
        }

        public void Stop(bool complete = false)
        {
            var animation = DoMotion.GetAnimation(id);
            animation?.Stop(complete);
        }
        
        public static implicit operator ulong(AnimationId animation) => animation.id;
    }
    
    public readonly struct AnimationId<T> where T : struct, IEquatable<T>
    {
        public readonly ulong id;

        public AnimationId(ulong id)
        {
            this.id = id;
        }
        
        public bool HasStarted(out bool started)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                started = animation.Started;
                return true;
            }

            started = false;
            return false;
        }
        public bool IsPlaying(out bool playing)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                playing = animation.Playing;
                return true;
            }

            playing = false;
            return false;
        }
        public bool IsStopped(out bool stopped)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                stopped = animation.Stopped;
                return true;
            }

            stopped = false;
            return false;
        }
        public bool IsCompleted(out bool completed)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                completed = animation.Completed;
                return true;
            }

            completed = false;
            return false;
        }
        public bool IsActive(out bool active)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                active = animation.Active;
                return true;
            }

            active = false;
            return false;
        }

        public bool IsPaused(out bool paused)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                paused = animation.Paused;
                return true;
            }

            paused = false;
            return false;
        }

        public bool IsAutoPlay(out bool autoPlay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                autoPlay = animation.AutoPlay;
                return true;
            }

            autoPlay = false;
            return false;
        }

        public AnimationId<T> OnStart(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.OnStart(callback);
            }

            return this;
        }

        public AnimationId<T> OnPlay(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.OnPlay(callback);
            }

            return this;
        }

        public AnimationId<T> OnPause(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.OnPause(callback);
            }

            return this;
        }

        public AnimationId<T> OnStop(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.OnStop(callback);
            }

            return this;
        }

        public AnimationId<T> OnComplete(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.OnComplete(callback);
            }

            return this;
        }

        public bool GetDelay(out float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                delay = animation.Delay;
                return true;
            }

            delay = 0f;
            return false;
        }
        
        public AnimationId<T> SetAutoPlay(bool autoPlay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.SetAutoPlay(autoPlay);
            }

            return this;
        }

        public AnimationId<T> SetDelay(float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.SetDelay(delay);
            }

            return this;
        }

        public void Play()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.Play();
            }
        }

        public void Pause()
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.Pause();
            }
        }

        public void Stop(bool complete = false)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.Stop(complete);
            }
        }

        public AnimationId<T> OnStep(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T>)
            {
                animation.OnStep(callback);
            }

            return this;
        }

        public AnimationId<T> OnLoop(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T> tAnimation)
            {
                tAnimation.OnLoop(callback);
            }

            return this;
        }

        public AnimationId<T> OnInterval(Action callback)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T> tAnimation)
            {
                tAnimation.OnInterval(callback);
            }

            return this;
        }

        public AnimationId<T> SetLoops(int loops, LoopType type = LoopType.Restart)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T> tAnimation)
            {
                tAnimation.SetLoops(loops, type);
            }

            return this;
        }

        public AnimationId<T> SetInterval(int interval, float delay)
        {
            var animation = DoMotion.GetAnimation(id);
            if (animation is Animation<T> tAnimation)
            {
                tAnimation.SetInterval(interval, delay);
            }

            return this;
        }
        
        public static implicit operator ulong(AnimationId<T> animation) => animation.id;
        public static implicit operator AnimationId(AnimationId<T> animation) => new (animation.id);
    }
    
    public abstract class Animation
    {
        public ulong Id { get; internal set; }

        private bool _started;
        internal bool Started
        {
            get => _started;
            private set
            {
                if (_started == value) return;
                if (value)
                {
                    AboutToStartCallback?.Invoke();
                }
                _started = value;
                if (value)
                {
                    StartCallback?.Invoke();
                }
            }
        }
        private bool _playing;
        internal bool Playing
        {
            get => _playing;
            private set
            {
                if (_playing == value) return;
                
                _playing = value;
                if (value)
                {
                    PlayCallback?.Invoke();
                }
            }
        }
        private bool _stopped;
        internal bool Stopped
        {
            get => _stopped;
            private set
            {
                if (_stopped == value) return;
                
                _stopped = value;
                if (value)
                {
                    StopCallback?.Invoke();
                }
            }
        }
        private bool _completed;
        internal bool Completed
        {
            get => _completed;
            private set
            {
                if (_completed == value) return;
                
                _completed = value;
                if (value)
                {
                    CompleteCallback?.Invoke();
                }
            }
        }

        internal bool Active => !Completed && !Stopped;
        internal bool Paused => Active && Started && !Playing;
        
        private bool _autoPlay;
        internal bool AutoPlay
        {
            get => _autoPlay;
            private set
            {
                if (Started) return;

                _autoPlay = value;
            }
        }
        
        private Action AboutToStartCallback { get; set; }
        private Action StartCallback { get; set; }
        private Action PlayCallback { get; set; }
        private Action PauseCallback { get; set; }
        private Action StopCallback { get; set; }
        private Action StepCallback { get; set; }
        private Action CompleteCallback { get; set; }

        private float _delay;
        internal float Delay
        {
            get => _delay;
            private set
            {
                if (Started) return;
                
                _delay = value;
            }
        }

        private float Time { get; set; }

        public virtual void SetAutoPlay(bool autoPlay) => AutoPlay = autoPlay;

        // TODO: Expose in every ID struct
        public void OnAboutToStart(Action callback) => AboutToStartCallback = callback;
        public void OnStart(Action callback) => StartCallback = callback;

        public void OnPlay(Action callback) => PlayCallback = callback;

        public void OnPause(Action callback) => PauseCallback = callback;

        public void OnStop(Action callback) => StopCallback = callback;
        public void OnStep(Action callback) => StepCallback = callback;

        public void OnComplete(Action callback) => CompleteCallback = callback;

        public virtual void SetDelay(float delay) => Delay = delay;

        internal virtual void Reset()
        {
            AboutToStartCallback = null;
            StartCallback = null;
            PlayCallback = null;
            PauseCallback = null;
            StopCallback = null;
            StepCallback = null;
            CompleteCallback = null;

            Started = false;
            Playing = false;
            Stopped = false;
            Completed = false;

            AutoPlay = true;
            
            Delay = 0;
            
            Time = 0;
        }

        public virtual void Play()
        {
            Started = true;
            Playing = true;
        }
        
        public virtual void Pause()
        {
            Playing = false;
        }

        public void Stop(bool complete)
        {
            if (!Active) return;
            
            OnStop(complete);

            Playing = false;
            Stopped = true;

            if (complete)
            {
                Completed = true;
            }
        }

        protected abstract void OnStop(bool complete);

        internal void Step(float deltaTime)
        {
            if (!Active) return;
            if (!Started) return;
            if (!Playing) return;

            Time += deltaTime;
            if (Time < Delay)
            {
                return;
            }

            var result = Tick(deltaTime);
            StepCallback?.Invoke();
            
            if (result)
            {
                Completed = true;
            }
        }

        protected abstract bool Tick(float deltaTime);
    }
    
    public abstract class Animation<T> : Animation where T : struct, IEquatable<T>
    {
        protected Func<T> Getter { get; private set; }
        protected Action<T> Setter { get; private set; }
        
        protected T Origin { get; private set; }
        protected T Target { get; private set; }
        
        private Action LoopCallback { get; set; }
        private Action IntervalCallback { get; set; }

        private int _loopsCount;
        private int LoopsCount
        {
            get => _loopsCount;
            set
            {
                if (Started) return;

                _loopsCount = value;
            }
        }

        private LoopType _loopType;
        private LoopType LoopType
        {
            get => _loopType;
            set
            {
                if (Started) return;
                
                _loopType = value;
            }
        }

        private int _interval;
        private int Interval
        {
            get => _interval;
            set
            {
                if (Started) return;

                _interval = value;
            }
        }

        private float _intervalDelay;
        private float IntervalDelay
        {
            get => _intervalDelay;
            set
            {
                if (Started) return;
                
                _intervalDelay = value;
            }
        }
        
        private int Loop { get; set; }
        private bool IsInterval { get; set; }
        private float Accum { get; set; }

        public T Velocity { get; protected set; }
        
        public void OnLoop(Action callback) => LoopCallback = callback;

        public void OnInterval(Action callback) => IntervalCallback = callback;

        public void SetLoops(int loops, LoopType loopType)
        {
            LoopsCount = loops;
            LoopType = loopType;
        }

        public void SetInterval(int interval, float delay)
        {
            Interval = interval;
            IntervalDelay = delay;
        }

        internal override void Reset()
        {
            base.Reset();
            Getter = null;
            Setter = null;
            Origin = default;
            Target = default;
            LoopCallback = null;
            IntervalCallback = null;
            IsInterval = false;
            Accum = 0;
            
            LoopsCount = 1;
            LoopType = LoopType.Restart;
            Interval = 0;
            IntervalDelay = 0;
            
            Loop = 0;
            
            Velocity = default;
        }
        
        internal virtual void Setup(Func<T> getter, Action<T> setter, T target)
        {
            Origin = getter();
            Target = target;
            Getter = getter;
            Setter = setter;
        }

        private void SwapOriginAndTarget()
        {
            (Origin, Target) = (Target, Origin);
        }

        protected abstract void PrepareForLoop();

        protected override void OnStop(bool complete)
        {
            if (!complete) return;

            Setter(Target);
        }

        protected override bool Tick(float deltaTime)
        {            
            if (LoopsCount == 0)
            {
                return true;
            }

            if (IsInterval)
            {
                if (Accum < IntervalDelay)
                {
                    Accum += deltaTime;
                    return false;
                }

                IsInterval = false;
                Accum = 0;
            }

            var value = Getter();
            var done = Tick(deltaTime, ref value);
            
            Setter(value);
            
            if (!done)
            {
                return false;
            }
            
            Loop++;
            IsInterval = Interval > 0 && Loop % Interval == 0;

            if (LoopsCount > 0 && Loop >= LoopsCount)
            {
                LoopCallback?.Invoke();
                if (IsInterval)
                {
                    IntervalCallback?.Invoke();
                }

                return true;
            }
                
            if (LoopType == LoopType.PingPong)
            {
                SwapOriginAndTarget();
            }
                
            PrepareForLoop();
            Setter(Origin);
            
            LoopCallback?.Invoke();
            if (IsInterval)
            {
                IntervalCallback?.Invoke();
            }
            
            return false;
        }

        protected abstract bool Tick(float deltaTime, ref T value);
    }
}