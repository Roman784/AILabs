namespace Gameplay
{
    public class NpcState
    {
        protected readonly NPC _npc;
        protected readonly  NpcStateHandler _handler;

        public NpcState(NPC npc, NpcStateHandler stateHandler)
        {
            _npc = npc;
            _handler = stateHandler;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}
