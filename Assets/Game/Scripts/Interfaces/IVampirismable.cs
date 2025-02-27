namespace Game.Scripts.Interfaces
{
    public interface IVampirismable
    {
        public float Coefficient { get; }

        public bool IsWorking { get; }

        public bool SetTrueVampirismState();

        public bool SetFalseVampirismState();

        public void SetCoefficient(float value);
    }
}