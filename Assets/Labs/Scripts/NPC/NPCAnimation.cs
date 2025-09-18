using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class NPCAnimation
    {
        private enum AnimationState
        {
            None,
            Idle,
            Walk
        }

        private Animator _animator;

        private Dictionary<AnimationState, string> _statesMap = new()
        {
            { AnimationState.Idle, "Idle" },
            { AnimationState.Walk, "Walk" }
        };

        public NPCAnimation(Animator animator)
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

        private void SetState(AnimationState state)
        {
            _animator.CrossFade(_statesMap[state], 0.25f);
        }
    }
}
