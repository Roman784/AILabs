namespace Gameplay
{
    public class InteractWithNpcPlayerState : PlayerState
    {
        public InteractWithNpcPlayerState(Player player, PlayerStateHandler stateHandler) : base(player, stateHandler)
        {
        }

        public override void Enter()
        {
            if (_player.Interaction.TryInteractWithNpc())
                _player.Animation.SetCommunicationAnimation();

            _handler.SetIdleState();
        }
    }
}
