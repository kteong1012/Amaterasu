namespace Game
{
    public abstract class UnitAI
    {
        public virtual void Init(UnitAIComponent aIComponent)
        {
        }
        public abstract void Act(UnitAIComponent aIComponent);
    }
}