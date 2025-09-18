using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerAnimation
    {
        private enum AnimationState
        {
            None,
            Idle,
            Walk,
            PickUp,
            Communication
        }

        private Animator _animator;

        private Dictionary<AnimationState, string> _statesMap = new()
        {
            { AnimationState.Idle, "Idle" },
            { AnimationState.Walk, "Walk" },
            { AnimationState.PickUp, "PickUp" },
            { AnimationState.Communication, "Communication" }
        };

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void SetIdleAnimation()
        {
            SetState(AnimationState.Idle);
        }

        public void SetWalkAnimation()
        {
            SetState(AnimationState.Walk);
        }

        public void SetPickUpItemAnimation()
        {
            SetState(AnimationState.PickUp);
        }

        public void SetCommunicationAnimation()
        {
            SetState(AnimationState.Communication);
        }

        private void SetState(AnimationState state)
        {
            _animator.CrossFade(_statesMap[state], 0.25f);
        }
    }
}
