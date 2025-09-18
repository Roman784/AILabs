using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class NpcStateHandler
    {
        private Dictionary<Type, NpcState> _statesMap;
        private NpcState _currentState;

        public NpcStateHandler(NPC npc)
        {
            _statesMap = new Dictionary<Type, NpcState>();
            _statesMap[typeof(IdleNpcState)] = new IdleNpcState(npc, this);
            _statesMap[typeof(WanderNpcState)] = new WanderNpcState(npc, this);
            _statesMap[typeof(StopNpcState)] = new StopNpcState(npc, this);

            // Initial state.
            SetWanderState();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void SetIdleState()
        {
            var state = GetState<IdleNpcState>();
            SetState(state);
        }

        public void SetWanderState()
        {
            var state = GetState<WanderNpcState>();
            SetState(state);
        }

        public void SetStopState()
        {
            var state = GetState<StopNpcState>();
            SetState(state);
        }

        public void SetState(NpcState behavior)
        {
            _currentState?.Exit();

            _currentState = behavior;
            _currentState.Enter();
        }

        private NpcState GetState<T>() where T : NpcState
        {
            return _statesMap[typeof(T)];
        }
    }
}
