using UnityEngine;

namespace Gameplay
{
    public class EnemyStateHandler : NpcStateHandler
    {
        private readonly Enemy _enemy;

        public EnemyStateHandler(Enemy enemy) : base(enemy)
        {
            _enemy = enemy;

            _statesMap[typeof(ChaseEnemyState)] = new ChaseEnemyState(enemy, this);
            _statesMap[typeof(AttackEnemyState)] = new AttackEnemyState(enemy, this);
        }

        public new void Update()
        {
            base.Update();

            if (_enemy.IsInAggressionRange(_enemy.Player.position) &&
                _currentState is not ChaseEnemyState &&
                _currentState is not AttackEnemyState)
            {
                SetChaseState(_enemy.Player);
            }
        }

        public void SetChaseState(Transform target)
        {
            var state = GetState<ChaseEnemyState>() as ChaseEnemyState;
            state.SetTarget(target);

            SetState(state);
        }
        
        public void SetAttackState(Transform target)
        {
            var state = GetState<AttackEnemyState>() as AttackEnemyState;
            state.SetTarget(target);

            SetState(state);
        }
    }
}
