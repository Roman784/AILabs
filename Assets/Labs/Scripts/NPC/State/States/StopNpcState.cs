using UnityEngine;

namespace Gameplay
{
    public class StopNpcState : NpcState
    {
        private const float STOP_TIME_RANGE = 3f;
        private float _timeToEnd;

        public StopNpcState(NPC npc, NpcStateHandler stateHandler) : base(npc, stateHandler)
        {
        }

        public override void Enter()
        {
            _timeToEnd = Time.time + Random.Range(0f, STOP_TIME_RANGE);
        }

        public override void Update()
        {
            if (_npc.IsPlayerInRange)
                _handler.SetIdleState();
            else if (_timeToEnd < Time.time)
                _handler.SetWanderState();
        }
    }
}
