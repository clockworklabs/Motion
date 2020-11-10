namespace Motion
{    
    public struct Tween
    {
        public Ease ease;
        public float duration;

        public static Tween Default = new Tween
        {
            ease = Ease.Back,
            duration = 1
        };
    }
}