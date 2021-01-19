using System;

namespace Motion
{
    public delegate void RefAction<T>(ref T value) where T : struct;
    
    public abstract class Animation
    {
        private static uint nextId;

        public uint ID { get; }

        private bool started;
        public bool Started
        {
            get => started;
            private set
            {
                if (started == value) return;
                
                started = value;
                if (value)
                {
                    OnStartCallback?.Invoke();
                }
            }
        }
        private bool playing;
        public bool Playing
        {
            get => playing;
            private set
            {
                if (playing == value) return;
                
                playing = value;
                if (value)
                {
                    OnPlayCallback?.Invoke();
                }
            }
        }
        private bool stopped;
        public bool Stopped
        {
            get => stopped;
            private set
            {
                if (stopped == value) return;
                
                stopped = value;
                if (value)
                {
                    OnStopCallback?.Invoke();
                }
            }
        }
        private bool completed;
        public bool Completed
        {
            get => completed;
            private set
            {
                if (completed == value) return;
                
                completed = value;
                if (value)
                {
                    OnCompleteCallback?.Invoke();
                }
            }
        }

        public bool Valid { get; protected set; }
        public bool Active => Valid && !Completed && !Stopped;
        public bool Paused => Active && Started && !Playing;
        
        private bool autoPlay;
        public bool AutoPlay
        {
            get => autoPlay;
            protected set
            {
                if (Started) return;

                autoPlay = value;
            }
        }

        private object owner;
        public object Owner
        {
            get => owner;
            protected set
            {
                if (Started) return;

                owner = value;
            }
        }
        
        private Action onStartCallback;
        public Action OnStartCallback
        {
            get => onStartCallback;
            protected set
            {
                if (Started) return;
                
                onStartCallback = value;
            }
        }
        private Action onPlayCallback;
        public Action OnPlayCallback
        {
            get => onPlayCallback;
            protected set
            {
                if (Started) return;
                
                onPlayCallback = value;
            }
        }
        private Action onPauseCallback;
        public Action OnPauseCallback
        {
            get => onPauseCallback;
            protected set
            {
                if (Started) return;
                
                onPauseCallback = value;
            }
        }
        private Action onStopCallback;
        public Action OnStopCallback
        {
            get => onStopCallback;
            protected set
            {
                if (Started) return;
                
                onStopCallback = value;
            }
        }
        private Action onCompleteCallback;
        public Action OnCompleteCallback {
            get => onCompleteCallback;
            protected set
            {
                if (Started) return;
                
                onCompleteCallback = value;
            }
        }

        private float delay;
        public float Delay
        {
            get => delay;
            protected set
            {
                if (Started) return;
                
                delay = value;
            }
        }

        private float Time { get; set; }

        protected Animation()
        {
            ID = nextId++;
        }

        public virtual Animation SetAutoPlay(bool autoPlay)
        {
            AutoPlay = autoPlay;

            return this;
        }

        public virtual Animation SetOwner(object owner)
        {
            Owner = owner;

            return this;
        }

        public Animation OnStart(Action callback)
        {
            OnStartCallback = callback;

            return this;
        }

        public Animation OnPlay(Action callback)
        {
            OnPlayCallback = callback;

            return this;
        }

        public Animation OnPause(Action callback)
        {
            OnPauseCallback = callback;

            return this;
        }

        public Animation OnStop(Action callback)
        {
            OnStopCallback = callback;

            return this;
        }

        public Animation OnComplete(Action callback)
        {
            OnCompleteCallback = callback;

            return this;
        }

        public virtual Animation SetDelay(float delay)
        {
            Delay = delay;

            return this;
        }

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
            Valid = false;

            AutoPlay = true;
            
            Owner = null;
            
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

        public void Stop(bool complete) => Stop(complete, null);
        public void Stop(object owner) => Stop(false, owner);
        public void Stop(bool complete = false, object owner = null)
        {
            if (!Active) return;
            if (Owner != null && Owner != owner) return;
            
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
        private Func<T> Getter { get; set; }
        private Action<T> Setter { get; set; }
        
        protected T Origin { get; private set; }
        protected T Target { get; private set; }
        
        private Action onStepCallback;
        public Action OnStepCallback {
            get => onStepCallback;
            protected set
            {
                if (Started) return;
                
                onStepCallback = value;
            }
        }
        private Action onLoopCallback;
        public Action OnLoopCallback
        {
            get => onLoopCallback;
            protected set
            {
                if (Started) return;
                
                onLoopCallback = value;
            }
        }
        private Action onIntervalCallback;
        public Action OnIntervalCallback {
            get => onIntervalCallback;
            protected set
            {
                if (Started) return;
                
                onIntervalCallback = value;
            }
        }

        private int loopsCount;
        public int LoopsCount
        {
            get => loopsCount;
            protected set
            {
                if (Started) return;

                loopsCount = value;
            }
        }

        private LoopType loopType;
        public LoopType LoopType
        {
            get => loopType;
            protected set
            {
                if (Started) return;
                
                loopType = value;
            }
        }

        private int interval;
        public int Interval
        {
            get => interval;
            protected set
            {
                if (Started) return;

                interval = value;
            }
        }

        private float intervalDelay;
        public float IntervalDelay
        {
            get => intervalDelay;
            protected set
            {
                if (Started) return;
                
                intervalDelay = value;
            }
        }
        
        private int Loop { get; set; }
        private bool IsInterval { get; set; }
        private float Accum { get; set; }
        
        public Animation<T> OnStep(Action callback)
        {
            OnStepCallback = callback;

            return this;
        }
        public Animation<T> OnLoop(Action callback)
        {
            OnLoopCallback = callback;

            return this;
        }

        public Animation<T> OnInterval(Action callback)
        {
            OnIntervalCallback = callback;

            return this;
        }

        public Animation<T> SetLoops(int loops, LoopType loopType = LoopType.Restart)
        {
            LoopsCount = loops;
            LoopType = loopType;

            return this;
        }

        public Animation<T> SetInterval(int interval, float delay = 0)
        {
            Interval = interval;
            IntervalDelay = delay;

            return this;
        }

        public T UpdateTarget(RefAction<T> update)
        {
            if (!Active)
            {
                return Target;
            }
            
            var target = Target;
            update?.Invoke(ref target);
            Target = target;

            return Target;
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
        
        protected void Setup(Func<T> getter, Action<T> setter, T origin, T target)
        {
            Valid = true;
            
            Origin = origin;
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