using System;

namespace Motion
{
    public delegate void RefAction<T>(ref T value) where T : struct;
    
    public readonly struct AnimationId
    {
        public readonly uint id;

        public AnimationId(uint id)
        {
            this.id = id;
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
        
        public static implicit operator uint(AnimationId animation) => animation.id;
    }
    
    public readonly struct AnimationId<T> where T : struct, IEquatable<T>
    {
        public readonly uint id;

        public AnimationId(uint id)
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
            if (animation is Animation<T> tAnimation)
            {
                tAnimation.OnStep(callback);
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
        
        public static implicit operator uint(AnimationId<T> animation) => animation.id;
        public static implicit operator AnimationId(AnimationId<T> animation) => new AnimationId(animation.id);
    }
    
    public abstract class Animation
    {
        public uint Id { get; internal set; }

        private bool _started;
        internal bool Started
        {
            get => _started;
            private set
            {
                if (_started == value) return;
                
                _started = value;
                if (value)
                {
                    OnStartCallback?.Invoke();
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
                    OnPlayCallback?.Invoke();
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
                    OnStopCallback?.Invoke();
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
                    OnCompleteCallback?.Invoke();
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
        
        private Action _onStartCallback;

        private Action OnStartCallback
        {
            get => _onStartCallback;
            set
            {
                if (Started) return;
                
                _onStartCallback = value;
            }
        }
        private Action _onPlayCallback;
        private Action OnPlayCallback
        {
            get => _onPlayCallback;
            set
            {
                if (Started) return;
                
                _onPlayCallback = value;
            }
        }
        private Action _onPauseCallback;
        private Action OnPauseCallback
        {
            get => _onPauseCallback;
            set
            {
                if (Started) return;
                
                _onPauseCallback = value;
            }
        }
        private Action _onStopCallback;
        private Action OnStopCallback
        {
            get => _onStopCallback;
            set
            {
                if (Started) return;
                
                _onStopCallback = value;
            }
        }
        private Action _onCompleteCallback;
        private Action OnCompleteCallback {
            get => _onCompleteCallback;
            set
            {
                if (Started) return;
                
                _onCompleteCallback = value;
            }
        }

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

        internal virtual void SetAutoPlay(bool autoPlay) => AutoPlay = autoPlay;

        internal void OnStart(Action callback) => OnStartCallback = callback;

        internal void OnPlay(Action callback) => OnPlayCallback = callback;

        internal void OnPause(Action callback) => OnPauseCallback = callback;

        internal void OnStop(Action callback) => OnStopCallback = callback;

        internal void OnComplete(Action callback) => OnCompleteCallback = callback;

        internal virtual void SetDelay(float delay) => Delay = delay;

        internal virtual void Reset()
        {
            OnStartCallback = null;
            OnPlayCallback = null;
            OnPauseCallback = null;
            OnStopCallback = null;
            OnCompleteCallback = null;

            Started = false;
            Playing = false;
            Stopped = false;
            Completed = false;

            AutoPlay = true;
            
            Delay = 0;
            
            Time = 0;
        }

        internal virtual void Play()
        {
            Started = true;
            Playing = true;
        }
        
        internal virtual void Pause()
        {
            Playing = false;
        }

        internal void Stop(bool complete)
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

            if (Paused) return;

            Time += deltaTime;
            if (Time < Delay)
            {
                return;
            }

            var result = Tick(deltaTime);
            
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
        
        private Action _onStepCallback;
        private Action OnStepCallback {
            get => _onStepCallback;
            set
            {
                if (Started) return;
                
                _onStepCallback = value;
            }
        }
        private Action _onLoopCallback;
        private Action OnLoopCallback
        {
            get => _onLoopCallback;
            set
            {
                if (Started) return;
                
                _onLoopCallback = value;
            }
        }
        private Action _onIntervalCallback;
        private Action OnIntervalCallback {
            get => _onIntervalCallback;
            set
            {
                if (Started) return;
                
                _onIntervalCallback = value;
            }
        }

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
        
        internal void OnStep(Action callback) => OnStepCallback = callback;
        
        internal void OnLoop(Action callback) => OnLoopCallback = callback;

        internal void OnInterval(Action callback) => OnIntervalCallback = callback;

        internal void SetLoops(int loops, LoopType loopType)
        {
            LoopsCount = loops;
            LoopType = loopType;
        }

        internal void SetInterval(int interval, float delay)
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
            OnStepCallback = null;
            OnLoopCallback = null;
            OnIntervalCallback = null;
            IsInterval = false;
            Accum = 0;
            
            LoopsCount = 1;
            LoopType = LoopType.Restart;
            Interval = 0;
            IntervalDelay = 0;
            
            Loop = 0;
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
            var temp = Origin;
            Origin = Target;
            Target = temp;
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
            
            OnStepCallback?.Invoke();
            
            Setter(value);
            
            if (!done)
            {
                return false;
            }
            
            Loop++;
            IsInterval = Interval > 0 && Loop % Interval == 0;

            if (LoopsCount > 0 && Loop >= LoopsCount)
            {
                OnLoopCallback?.Invoke();
                if (IsInterval)
                {
                    OnIntervalCallback?.Invoke();
                }

                return true;
            }
                
            if (LoopType == LoopType.PingPong)
            {
                SwapOriginAndTarget();
            }
                
            PrepareForLoop();
            Setter(Origin);
            
            OnLoopCallback?.Invoke();
            if (IsInterval)
            {
                OnIntervalCallback?.Invoke();
            }
            
            return false;
        }

        protected abstract bool Tick(float deltaTime, ref T value);
    }
}