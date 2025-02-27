namespace Game.Scripts.EnemyComponents.Interfaces
{
    public interface IEnemyAnimation
    {
        public bool IsAttacking { get; }
        public int AttackVariantsCount { get; }
        public void Spawn();
        public void Move(bool isMoving);
        public void TakeHit();
        public void Death();
        public void Attack(int attackVariant);
        public void ResetAttackState();
    }
}