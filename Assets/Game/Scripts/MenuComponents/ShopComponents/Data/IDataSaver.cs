namespace Game.Scripts.MenuComponents.ShopComponents.Data
{
    public interface IDataSaver
    {
        public void Save();

        public bool TryLoad();
    }
}