using UnityEngine;

namespace Gameplay
{
    public class WanderNpcState : NpcState
    {
        private Vector3 _currentDestination;

        public WanderNpcState(NPC npc, NpcStateHandler stateHandler) : base(npc, stateHandler)
        {
        }

        public override void Enter()
        {
            _npc.Animation.SetWalkAnimation();
            SetNewDestination();
        }

        public override void Update()
        {
            if (_npc.IsPlayerInRange)
                _handler.SetIdleState();
            else if (_npc.Movement.IsPositionReached)
                _handler.SetStopState();
        }

        public override void Exit()
        {
            _npc.Movement.StopMovement();
        }

        private void SetNewDestination()
        {
            _currentDestination = Random.insideUnitSphere * _npc.WanderRange + _npc.WanderCenterPosition;
            _npc.Movement.MoveTo(_currentDestination);
        }
    }
}
