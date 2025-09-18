using UnityEngine;

namespace Gameplay
{
    public class IdleNpcState : NpcState
    {
        public IdleNpcState(NPC npc, NpcStateHandler stateHandler) : base(npc, stateHandler)
        {
        }

        public override void Enter()
        {
            _npc.Animation.SetIdleAnimation();
        }

        public override void Update()
        {
            if (!_npc.IsPlayerInRange)
                _handler.SetWanderState();
        }
    }
}
