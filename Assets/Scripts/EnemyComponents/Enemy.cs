using System;
using System.Collections;
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
using UnityEngine.AI;
using Weapons;

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
        
        private IAttackBehavior _attackBehavior;
        private IEnemyMovement _movement;
        private IEnemyRotation _rotation;
        private IEnemyAttack _enemyAttack;
        private IEnemyCollider _enemyCollider;
        private IEnemyEffects _enemyEffects;
        
        private BaseProjectileSpawner _projectileSpawner;
        private RangedProjectileSpawner _rangedSpawner;
        private HybridProjectileSpawner _hybridSpawner;
        
        private ICoroutineRunner _coroutineRunner;
        private Coroutine _movementCoroutine;     
        private bool _spawnCompleted = false;
        private NavMeshAgent _agent;

        public EnemyData Data => _data;
        public Collider Collider => _collider;
        public EnemyAnimationController AnimationAnimationController => _animationController;
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
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            if (Health != null)
            {
                Health.Death += OnDeath;
            }

            if (_coroutineRunner != null && _movementCoroutine == null)
            {
                _movementCoroutine = _coroutineRunner.StartCoroutine(AttackCoroutine());
            }

            Enabled?.Invoke(this);
        }

        private void OnDisable()
        {
            if (Health != null)
            {
                Health.Death -= OnDeath;
            }

            if (_coroutineRunner != null && _movementCoroutine != null)
            {
                _coroutineRunner.StopCoroutine(_movementCoroutine);
                _movementCoroutine = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Weapon weapon))
            {
                Health.Lose(weapon.WeaponData.Damage);

                if (Health.IsDead)
                {
                    _player.GetExperience(Data.Experience);
                    _player.GetMoney(Data.Money);
                    _enemyEffects.Death();
                }
            }
        }

        private void Start()
        {
            _agent.enabled = true;
        }

        private void Update()
        {
            MoveAndRotate();
        }

        public void InitializeComponents(Player player, EnemyData enemyData, EffectsPool pool, PoolManager poolManager, ICoroutineRunner coroutineRunner)
        {
            _player = player;
            _data = enemyData;
            _coroutineRunner = coroutineRunner;
            
            if (_animationController == null)
            {
                _animationController = new EnemyAnimationController(_animator, _data.EnemyType);
            }
            
            _enemyCollider = new EnemyCollider(this, _player);
            _movement = new EnemyMovement(transform, _data.MoveSpeed, AnimationAnimationController, _agent);
            _rotation = new EnemyRotation(transform, _data.RotationSpeed);
            _enemyEffects.Initialize(_data, pool, coroutineRunner);

            if (_data.BaseAttackType.Type == AttackType.Ranged)
            {
                _projectileSpawner = _rangedSpawner;
            }
            else if(_data.BaseAttackType.Type == AttackType.Hybrid)
            {
                _projectileSpawner = _hybridSpawner;
            }
            
            _projectileSpawner?.Initialize(_data, _player, poolManager);
            
            _enemyAttack = new EnemyAttack(AnimationAnimationController, transform, _player, _data.AttackCooldown, _data.BaseAttackType, AnimationAnimationController.AttackVariantsCount);
            
            SetAttackBehavior();
            
            _spawnCompleted = false;
            AnimationAnimationController.Spawn();
            Health.InitMaxValue(_data.MaxHealth);
            
            if (_coroutineRunner != null && _movementCoroutine == null && gameObject.activeInHierarchy)
            {
                _movementCoroutine = _coroutineRunner.StartCoroutine(AttackCoroutine());
            }
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

        private void OnDeath()
        {
            AnimationAnimationController.Death();
            Dead?.Invoke(this);
        }
        
        private void MoveAndRotate()
        {
            if (_player == null)
                return;

            if (_agent.isActiveAndEnabled == false)
            {
                return;
            }

            _rotation.RotateTowards(_player.transform.position);

            if (!_spawnCompleted || AnimationAnimationController.IsAttacking)
            {
                _movement.StopMove();
                return;
            }

            if (_agent.isActiveAndEnabled)
            {
                _movement.Move(_player.transform.position);
                _movement.PlayMove();
            }
            else
            {
                _movement.StopMove();
            }
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
        
        private IEnumerator AttackCoroutine()
        {
            float updateInterval = 0.1f;
        
            while (true)
            {
                if(_player != null && _spawnCompleted && !AnimationAnimationController.IsAttacking)
                {
                    float distance = Vector3.Distance(transform.position, _player.transform.position);
                    _attackBehavior.HandleAttack(distance);
                }
            
                yield return new WaitForSeconds(updateInterval);
            }
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