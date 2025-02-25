public interface IGameplayVisitor
{
    public void Visit(MeleePlayer meleePlayerFactory);

    public void Visit(RangePlayer rangePlayerFactory);
}