namespace Gameplay
{
    public abstract class HelicopterControls
    {
        public float Pitch { get; protected set; }
        public float Roll { get; protected set; }
        public float Yaw { get; protected set; }
        public float Collective { get; protected set; }
        public float Throttle { get; protected set; }

        public abstract void HandleInput();
    }
}
