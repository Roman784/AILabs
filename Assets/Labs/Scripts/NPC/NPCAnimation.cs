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
            Walk,
            Attack
        }

        private Animator _animator;

        private Dictionary<AnimationState, string> _statesMap = new()
        {
            { AnimationState.Idle, "Idle" },
            { AnimationState.Walk, "Walk" },
            { AnimationState.Attack, "Attack" },
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

        public void SetAttackAnimation()
        {
            SetState(AnimationState.Attack);
        }

        private void SetState(AnimationState state)
        {
            _animator.CrossFade(_statesMap[state], 0.25f);
        }
    }
}
