using System;

namespace Motion
{
    public abstract class Animation
    {
        private static uint nextId;

        public Action OnLoopCallback { get; protected set; }
        public Action OnIntervalCallback { get; protected set; }
        public Action OnCompleteCallback { get; protected set; }
        
        public uint ID { get; }
        
        public bool Playing { get; private set; }
        public bool Completed { get; private set; }

        private bool manualStep;
        internal bool ManualStep
        {
            get => manualStep;
            set
            {
                if (Playing) return;
                
                manualStep = value;
            }
        }

        private float delay;
        public float Delay
        {
            get => delay;
            protected set
            {
                if (Playing) return;
                
                delay = value;
            }
        }

        private int loopsCount;
        public int LoopsCount
        {
            get => loopsCount;
            protected set
            {
                if (Playing) return;

                loopsCount = value;
            }
        }

        private LoopType loopType;
        public LoopType LoopType
        {
            get => loopType;
            protected set
            {
                if (Playing) return;
                
                loopType = value;
            }
        }

        private int interval;
        public int Interval
        {
            get => interval;
            protected set
            {
                if (Playing) return;

                interval = value;
            }
        }

        private float intervalDelay;
        public float IntervalDelay
        {
            get => intervalDelay;
            protected set
            {
                if (Playing) return;
                
                intervalDelay = value;
            }
        }

        private float Time { get; set; }
        protected int Loop { get; set; }

        protected Animation()
        {
            ID = nextId++;
        }

        internal virtual void Reset()
        {
            Playing = false;
            Completed = false;
            ManualStep = false;
            Delay = 0;
            LoopsCount = 1;
            LoopType = LoopType.Restart;
            Time = 0;
            Loop = 0;
            
            OnLoopCallback = null;
            OnCompleteCallback = null;
        }

        internal void Step(float deltaTime)
        {
            if (Completed) return;

            Playing = true;

            Time += deltaTime;
            if (Time < Delay)
            {
                return;
            }

            if (Tick(deltaTime))
            {
                Playing = false;
                Completed = true;
            }
        }

        protected abstract bool Tick(float deltaTime);
    }
    
    public abstract class Animation<T> : Animation where T : struct
    {
        protected Func<T> Getter { get; private set; }
        protected Action<T> Setter { get; private set; }
        
        protected T Origin { get; private set; }
        protected T Target { get; private set; }
        
        internal override void Reset()
        {
            base.Reset();
            Getter = null;
            Setter = null;
            Origin = default;
            Target = default;
        }
        
        internal void Setup(Func<T> getter, Action<T> setter, T target)
        {
            Getter = getter;
            Setter = setter;

            Origin = getter();
            Target = target;

            Setup();
        }

        protected abstract void Setup();

        protected void SwapOriginAndTarget()
        {
            var temp = Origin;
            Origin = Target;
            Target = temp;
        }
    }
}