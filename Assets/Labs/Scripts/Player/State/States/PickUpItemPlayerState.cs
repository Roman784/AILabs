namespace Gameplay
{
    public class PickUpItemPlayerState : PlayerState
    {
        public PickUpItemPlayerState(Player player, PlayerStateHandler stateHandler) : base(player, stateHandler)
        {
        }

        public override void Enter()
        {
            if (_player.Interaction.TryPickUpItem())
                _player.Animation.SetPickUpItemAnimation();

            _handler.SetIdleState();
        }
    }
}
