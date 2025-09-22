namespace Gameplay
{
    public class EnemyState : NpcState
    {
        protected readonly Enemy _enemy;
        protected new readonly EnemyStateHandler _handler;

        public EnemyState(Enemy enemy, EnemyStateHandler stateHandler) : base(enemy, stateHandler)
        {
            _enemy = enemy;
            _handler = stateHandler;
        }
    }
}
