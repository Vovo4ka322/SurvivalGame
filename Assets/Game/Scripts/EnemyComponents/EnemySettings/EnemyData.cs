using UnityEngine;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType;

namespace Game.Scripts.EnemyComponents.EnemySettings
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create New Enemy")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private Enemy _enemyPrefab;
        
        [Header("Setting type")]
        [SerializeField] private EnemyType _enemyType;
        [SerializeReference] private BaseEnemyAttackType _baseAttackType;
        
        [Header("Setting parameters")] 
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private int _experience;
        [SerializeField] private int _gold;
        [SerializeField] private int _points;
        
        [Header("Effects")]
        [SerializeField] private EffectData _spawnEffect;
        [SerializeField] private EffectData _hitEffect;
        [SerializeField] private EffectData _deathEffect;
        
        public EnemyType EnemyType => _enemyType;
        public Enemy EnemyPrefab => _enemyPrefab;
        public EffectData SpawnEffect => _spawnEffect;
        public EffectData HitEffect => _hitEffect;
        public EffectData DeathEffect => _deathEffect;
        public BaseEnemyAttackType BaseAttackType => _baseAttackType;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float Damage => _damage;
        public float MaxHealth => _maxHealth;
        public float AttackCooldown => _attackCooldown;
        public float AttackRange => _attackRange;
        public int Experience => _experience;
        public int Money => _gold;
        public int Points => _points;
    }
}