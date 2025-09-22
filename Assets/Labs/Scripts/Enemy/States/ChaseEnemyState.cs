using UnityEngine;

namespace Gameplay
{
    public class ChaseEnemyState : EnemyState
    {
        private Transform _target;

        public ChaseEnemyState(Enemy enemy, EnemyStateHandler stateHandler) : base(enemy, stateHandler)
        {
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public override void Enter()
        {
            _npc.Animation.SetWalkAnimation();
        }

        public override void Update()
        {
            _enemy.Movement.MoveTo(_target.position);

            if (!_enemy.IsInAggressionRange(_target.position))
                _handler.SetWanderState();
            else if (_enemy.IsInAttackRange(_target.position))
                _handler.SetAttackState(_target);
        }

        public override void Exit()
        {
            _enemy.Movement.StopMovement();
        }
    }
}
