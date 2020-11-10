namespace Motion
{
    public abstract class SpringAnimation<T> : Animation<T> where T : struct
    {
        private Spring spring;
        public Spring Spring
        {
            get => spring;
            private set
            {
                if (Playing) return;

                spring = value;
            }
        }
        
        public T Velocity { get; private set; }
        
        protected override void Setup()
        {
            SetSpring(Spring.Default);
        }

        public SpringAnimation<T> SetDelay(float delay)
        {
            Delay = delay;

            return this;
        }

        public SpringAnimation<T> SetLoops(int loops, LoopType loopType = LoopType.Restart)
        {
            LoopsCount = loops;
            LoopType = loopType;

            return this;
        }

        public SpringAnimation<T> SetSpring(Spring spring)
        {
            Spring = spring;

            return this;
        }

        public SpringAnimation<T> SetInitialVelocity(T velocity)
        {
            Velocity = velocity;

            return this;
        }

        protected override bool Tick(float deltaTime) {
            if (LoopsCount == 0)
            {
                return true;
            }
            
            if (Delay > 0)
            {
                Delay -= deltaTime;
                deltaTime = -Delay;
            }

            if (deltaTime <= 0)
            {
                return false;
            }

            // Object position and velocity.
            var value = Getter();
            var delta = Subtract(value, Target);
            if (SqrMagnitude(Velocity) < Spring.sqrRestSpeed && SqrMagnitude(delta) < Spring.sqrRestDelta)
            {
                LoopsCount--;

                if (LoopsCount == 0)
                {
                    Setter(Target);
                    return true;
                }
                
                if (LoopType == LoopType.PingPong)
                {
                    SwapOriginAndTarget();
                }
                
                Setter(Origin);
                
                return false;
            }
            
            // Spring stiffness, in kg / s^2
            var k = -Spring.stiffness;
            // Damping constant, in kg / s
            var d = -Spring.damping;
        
            var fSpring = Multiply(delta, k);
            var fDamping = Multiply(Velocity, d);
            var a = Multiply(Add(fSpring, fDamping), Spring.inverseMass);
            Velocity = Add(Velocity, Multiply(a, deltaTime));
            value = Add(value, Multiply(Velocity, deltaTime));
            
            Setter(value);
            
            return false;
        }

        protected abstract T Add(T a, T b);
        protected abstract T Subtract(T a, T b);
        protected abstract T Multiply(T a, float b);
        protected abstract float SqrMagnitude(T a);
    }
}