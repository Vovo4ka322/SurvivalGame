namespace Game.Scripts.EnemyComponents.Interfaces
{
    public interface IEnemyAnimation
    {
        public void Spawn();
        public void Move(bool isMoving);
        public void TakeHit();
        public void Death();
        public void Attack(int attackVariant);
        public void ResetAttackState();
    }
}