namespace Game.Scripts.MenuComponents.ShopComponents.Data
{
    public interface IDataProvider
    {
        public void Save();

        public bool TryLoad();
    }
}