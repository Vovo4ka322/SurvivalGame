namespace Player
{
    public interface IHealable
    {
        public bool IsHealState { get; }

        public void SetState(bool state);
    }
}