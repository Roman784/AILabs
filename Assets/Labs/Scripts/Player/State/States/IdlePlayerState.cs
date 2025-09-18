using UnityEngine;

namespace Gameplay
{
    public class IdlePlayerState : PlayerState
    {
        public IdlePlayerState(Player player, PlayerStateHandler stateHandler) : base(player, stateHandler)
        {
        }

        public override void Enter()
        {
            _player.Animation.SetIdleAnimation();
        }

        public override void Update()
        {
            if (_player.Input.IsMovementActive())
                _handler.SetWalkState();

            else if (_player.Input.IsInteractWithNpc())
                _handler.SetInteractWithNpcState();

            else if (_player.Input.IsPickUpItem())
                _handler.SetPickUpItemState();

            else if (_player.Input.IsDropItem())
                _handler.SetDropState();
        }
    }
}
