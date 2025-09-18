using UnityEngine;

namespace Gameplay
{
    public class WalkPlayerState : PlayerState
    {
        public WalkPlayerState(Player player, PlayerStateHandler stateHandler) : base(player, stateHandler)
        {
        }

        public override void Enter()
        {
            _player.Animation.SetWalkAnimation();
        }

        public override void Update()
        {
            if (_player.Input.IsMovementActive())
                _player.Movement.Move();

            else
                _handler.SetIdleState();
        }

        public override void Exit()
        {
            _player.Movement.StopMovement();
        }
    }
}
