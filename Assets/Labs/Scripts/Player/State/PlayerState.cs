namespace Gameplay
{
    public class PlayerState
    {
        protected readonly Player _player;
        protected readonly PlayerStateHandler _handler;

        public PlayerState(Player player, PlayerStateHandler stateHandler)
        {
            _player = player;
            _handler = stateHandler;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}
