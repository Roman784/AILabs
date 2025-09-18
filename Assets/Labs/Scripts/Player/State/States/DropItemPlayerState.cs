namespace Gameplay
{
    public class DropItemPlayerState : PlayerState
    {
        public DropItemPlayerState(Player player, PlayerStateHandler stateHandler) : base(player, stateHandler)
        {
        }

        public override void Enter()
        {
            if (_player.Interaction.TryDropItem())
                _player.Animation.SetPickUpItemAnimation();

            _handler.SetIdleState();
        }
    }
}
