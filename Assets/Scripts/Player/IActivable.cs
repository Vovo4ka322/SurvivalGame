public interface IActivable
{
    public bool IsActiveState { get; }

    public void SetState(bool state);
}