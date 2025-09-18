using UnityEngine;

namespace Gameplay
{
    public class WanderNpcState : NpcState
    {
        private const float FIELD_RANGE = 4f;

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
            _currentDestination = new Vector3(
                Random.Range(-FIELD_RANGE, FIELD_RANGE), 
                0f, 
                Random.Range(-FIELD_RANGE, FIELD_RANGE));
            _npc.Movement.MoveTo(_currentDestination);
        }
    }
}
