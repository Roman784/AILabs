using UnityEngine;

namespace Gameplay
{
    public class AttackEnemyState : EnemyState
    {
        private Transform _target;

        private Vector3 TargetLoseOffset => (_target.position - _enemy.transform.position) * 0.5f; 

        public AttackEnemyState(Enemy enemy, EnemyStateHandler stateHandler) : base(enemy, stateHandler)
        {
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public override void Enter()
        {
            _enemy.Animation.SetAttackAnimation();
        }

        public override void Update()
        {
            if (!_enemy.IsInAttackRange(_target.position - TargetLoseOffset))
                _handler.SetChaseState(_target);
        }
    }
}
