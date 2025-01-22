public interface IGameplayVisitor
{
    public void Visit(MeleePlayerFactory meleePlayerFactory);

    public void Visit(RangePlayerFactory rangePlayerFactory);
}