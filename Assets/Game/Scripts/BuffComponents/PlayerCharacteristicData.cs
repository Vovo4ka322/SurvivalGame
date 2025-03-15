namespace Game.Scripts.BuffComponents
{
    [System.Serializable]
    public class PlayerCharacteristicData
    {
        public float Health;
        public float Armor;
        public float Damage;
        public float AttackSpeed;
        public float MovementSpeed;
        public int HealthLevelImprovment;
        public int ArmorLevelImprovment;
        public int DamageLevelImprovment;
        public int AttackSpeedLevelImprovment;
        public int MovementSpeedLevelImprovment;

        public void InitLevelHealth(int level) => HealthLevelImprovment = level;

        public void InitLevelArmor(int level) => ArmorLevelImprovment = level;

        public void InitLevelDamage(int level) => DamageLevelImprovment = level;

        public void InitLevelAttackSpeed(int level) => AttackSpeedLevelImprovment = level;

        public void InitLevelMovementSpeed(int level) => MovementSpeedLevelImprovment = level;

        public void InitHealth(float health) => Health = health;

        public void InitArmor(float armor) => Armor = armor;

        public void InitDamage(float damage) => Damage = damage;

        public void InitAttackSpeed(float attackSpeed) => AttackSpeed = attackSpeed;

        public void InitMovementSpeed(float movementSpeed) => MovementSpeed = movementSpeed;
    }
}