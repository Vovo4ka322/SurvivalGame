using System;
using UnityEngine;
using EnemyComponents.Animations;
using EnemyComponents.EnemySettings;
using EnemyComponents.EnemySettings.Effects;
using EnemyComponents.EnemySettings.EnemyAttackType;
using EnemyComponents.EnemySettings.EnemyBehaviors;
using EnemyComponents.Interfaces;
using EnemyComponents.Projectiles;
using PlayerComponents;
using Pools;

namespace EnemyComponents
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyEffects))]
    public class Enemy : Character
    {
        [SerializeField] private EnemyData _data;
        
        private Player _player;
        private Animator _animator;
        private Collider _collider;
        private EnemyAnimationController _animationController;
        private PlayerNavigator _playerNavigator;
        
        private IAttackBehavior _attackBehavior;
        private IEnemyMovement _movement;
        private IEnemyRotation _rotation;
        private IEnemyAttack _enemyAttack;
        private IEnemyCollider _enemyCollider;
        private IEnemyEffects _enemyEffects;
        
        private BaseProjectileSpawner _projectileSpawner;
        private RangedProjectileSpawner _rangedSpawner;
        private HybridProjectileSpawner _hybridSpawner;
        
        private bool _spawnCompleted = false;
        
        public EnemyData Data => _data;
        public Collider Collider => _collider;
        public EnemyAnimationController AnimationAnimationController => _animationController;
        public PlayerNavigator PlayerNavigator => _playerNavigator;
        public IEnemyAttack EnemyAttack => _enemyAttack;
        public IEnemyMovement Movement => _movement;
        
        public event Action<Enemy> Dead;
        public event Action<Enemy> Enabled;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemyEffects = GetComponent<EnemyEffects>();
            _collider = GetComponent<Collider>();
            _animationController = new EnemyAnimationController(_animator, _data.EnemyType);
            _rangedSpawner = GetComponent<RangedProjectileSpawner>();
            _hybridSpawner = GetComponent<HybridProjectileSpawner>();
        }

        private void OnEnable()
        {
            if(Health != null)
            {
                Health.Death += OnDeath;
            }
            
            Enabled?.Invoke(this);
        }

        private void OnDisable()
        {
            if(Health != null)
            {
                Health.Death -= OnDeath;
            }
            
            Dead?.Invoke(this);
        }
        
        private void Update()
        {
            MovementAndAttack();
        }

        public void InitializeComponents(Player player, EnemyData enemyData, EffectsPool pool, PoolManager poolManager, ICoroutineRunner coroutineRunner)
        {
            _player = player;
            _data = enemyData;
            
            _enemyCollider = new EnemyCollider(this, _player);
            _movement = new EnemyMovement(transform, _data.MoveSpeed, AnimationAnimationController);
            //_movement = new EnemyAStarMovement(transform, _data.MoveSpeed, _data.RotationSpeed, AnimationAnimationController);
            _rotation = new EnemyRotation(transform, _data.RotationSpeed);
            _enemyEffects.Initialize(_data, pool, coroutineRunner);
            
            if(_data.BaseAttackType.Type == AttackType.Ranged)
            {
                _projectileSpawner = _rangedSpawner;
            }
            else if(_data.BaseAttackType.Type == AttackType.Hybrid)
            {
                _projectileSpawner = _hybridSpawner;
            }
            
            _projectileSpawner?.Initialize(_data, _player, poolManager);
            
            _enemyAttack = new EnemyAttack(AnimationAnimationController, transform, _player, _data.AttackCooldown, _data.BaseAttackType, AnimationAnimationController.AttackVariantsCount);
            _playerNavigator = new PlayerNavigator(_movement, _player.transform);
            
            SetAttackBehavior();
            
            _spawnCompleted = false;
            AnimationAnimationController.Spawn();
            //Health.InitMaxValue(_data.MaxHealth);
        }
        
        public float SetDamage()
        {
            return Data.Damage;
        }
        
        public void SpawnAnimationEnd()
        {
            _spawnCompleted = true;
        }

        public void AttackAnimationEnd()
        {
            AnimationAnimationController?.ResetAttackState();
        }

        private void OnTriggerEnter(Collider other)
        {
            _enemyCollider.HandleCollision(other);
        }

        private void OnDeath()
        {
            AnimationAnimationController.Death();
            Dead?.Invoke(this);
        }
        
        private void SetAttackBehavior()
        {
            switch (_data.BaseAttackType.Type)
            {
                case AttackType.Hybrid:
                    HybridEnemyAttackType hybridAttackType = _data.BaseAttackType as HybridEnemyAttackType;
                    
                    if (hybridAttackType != null)
                    {
                        _attackBehavior = new HybridAttack(this, hybridAttackType);
                    }
                    else
                    {
                        _attackBehavior = new MeleeAttack(this);
                    }
                    break;
                
                case AttackType.Ranged:
                    _attackBehavior = new RangedAttack(this);
                    break;
                
                default:
                    _attackBehavior = new MeleeAttack(this);
                    break;
            }
        }
        
        private void MovementAndAttack()
        {
            _rotation.RotateTowards(_player.transform.position);
            
            if (!_spawnCompleted || _player == null)
            {
                return;
            }

            if (AnimationAnimationController.IsAttacking)
            {
                _movement.StopMove();
                return;
            }

            float distance = Vector3.Distance(transform.position, _player.transform.position);
            _attackBehavior.HandleAttack(distance);
            
            /*if (!_spawnCompleted || _player == null)
            {
                return;
            }
            
            float distance = Vector3.Distance(transform.position, _player.transform.position);
            
            if (AnimationAnimationController.IsAttacking || distance <= _data.AttackRange * 0.9f)
            {
                _movement.StopMove();
                _rotation.RotateTowards(_player.transform.position);
            }
            else
            {
                _movement.Move(_player.transform.position);
            }
        
            _attackBehavior.HandleAttack(distance);*/
        }

        private void OnDrawGizmosSelected()
        {
            if(_data != null)
            {
                if(_data.BaseAttackType.Type == AttackType.Hybrid)
                {
                    HybridEnemyAttackType hybrid = _data.BaseAttackType as HybridEnemyAttackType;

                    if(hybrid != null)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireSphere(transform.position, hybrid.MeleeRange);
                        Gizmos.color = Color.blue;
                        Gizmos.DrawWireSphere(transform.position, hybrid.RangedRange);
                    }
                }
                else
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(transform.position, _data.AttackRange);
                }
            }
        }
    }
}