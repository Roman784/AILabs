using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class PlayerStateHandler
    {
        private Dictionary<Type, PlayerState> _statesMap;
        private PlayerState _currentState;

        public PlayerStateHandler(Player player)
        {
            _statesMap = new Dictionary<Type, PlayerState>();
            _statesMap[typeof(IdlePlayerState)] = new IdlePlayerState(player, this);
            _statesMap[typeof(WalkPlayerState)] = new WalkPlayerState(player, this);
            _statesMap[typeof(PickUpItemPlayerState)] = new PickUpItemPlayerState(player, this);
            _statesMap[typeof(InteractWithNpcPlayerState)] = new InteractWithNpcPlayerState(player, this);

            // Initial state.
            SetIdleState();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void SetIdleState()
        {
            var state = GetState<IdlePlayerState>();
            SetState(state);
        }

        public void SetWalkState()
        {
            var state = GetState<WalkPlayerState>();
            SetState(state);
        }

        public void SetPickUpItemState()
        {
            var state = GetState<PickUpItemPlayerState>();
            SetState(state);
        }

        public void SetInteractWithNpcState()
        {
            var state = GetState<InteractWithNpcPlayerState>();
            SetState(state);
        }

        public void SetState(PlayerState state)
        {
            _currentState?.Exit();

            _currentState = state;
            _currentState.Enter();
        }

        private PlayerState GetState<T>() where T : PlayerState
        {
            return _statesMap[typeof(T)];
        }
    }
}
