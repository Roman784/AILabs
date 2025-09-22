using UnityEngine;

namespace Gameplay
{
    public class Enemy : NPC
    {
        [SerializeField] private Transform _aggressionZoneCenterPoint;
        [SerializeField] private float _aggressionRange;
        [SerializeField] private float _attackRange;

        [Space]

        [SerializeField] private Transform _player;

        private EnemyStateHandler _stateHandler;

        public Vector3 AggressionZoneCenter => _aggressionZoneCenterPoint.position;
        public Transform Player => _player;

        private new void Awake()
        {
            base.Awake();

            _stateHandler = new EnemyStateHandler(this);
        }

        public bool IsInAggressionRange(Vector3 targetPosition)
        {
            return Vector3.Distance(targetPosition, AggressionZoneCenter) <= _aggressionRange;
        }

        public bool IsInAttackRange(Vector3 targetPosition)
        {
            return Vector3.Distance(transform.position, targetPosition) <= _attackRange;
        }

        private new void Update()
        {
            _stateHandler?.Update();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(WanderCenterPosition, WanderRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(AggressionZoneCenter, _aggressionRange);
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}
