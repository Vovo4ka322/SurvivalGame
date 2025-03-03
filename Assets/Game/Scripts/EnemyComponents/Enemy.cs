using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Game.Scripts.EnemyComponents.Animations;
using Game.Scripts.EnemyComponents.EnemySettings;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttackType;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.EnemyComponents.Projectiles;
using Game.Scripts.PoolComponents;
using Game.Scripts.PlayerComponents;
using Weapons;

namespace Game.Scripts.EnemyComponents
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyEffects))]
    public class Enemy : Character
    {
        [SerializeField] private EnemyData _data;
        
        private Player _player;
        private Vector3 _targetPosition;
        
        private Animator _animator;
        private EnemyAnimationController _animationController;
        private Collider _collider;
        
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
        private NavMeshAgent _agent;
        
        private bool _spawnCompleted = false;
        private bool _isDying = false;

        public EnemyData Data => _data;
        public Player Player => _player;
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
                Health.Death += OnDead;
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
                Health.Death -= OnDead;
            }

            if (_coroutineRunner != null && _movementCoroutine != null)
            {
                _coroutineRunner.StopCoroutine(_movementCoroutine);
                _movementCoroutine = null;
            }
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
            
            _animationController = new EnemyAnimationController(_animator, _data.EnemyType);
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
            
            _targetPosition = _player.transform.position;
            _spawnCompleted = false;
            _isDying = false;
            
            AnimationAnimationController.Spawn();
            SpawnAnimationEnd();
            Health.InitMaxValue(_data.MaxHealth);
            
            if (_coroutineRunner != null && _movementCoroutine == null && gameObject.activeInHierarchy)
            {
                _movementCoroutine = _coroutineRunner.StartCoroutine(AttackCoroutine());
            }
        }
        
        public void SetTargetPosition(Vector3 target)
        {
            _targetPosition = target;
        }
        
        public void TurnOnAgent()
        {
            _agent.enabled = true;
        }

        public void TurnOffAgent()
        {
            _agent.enabled = false;
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
        
        public void OnDeathAnimationEvent()
        {
            _enemyEffects.Death();
        }
        
        public void DeathAnimationEnd()
        {
            Dead?.Invoke(this);
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
                }
            }
        }

        private void OnDead()
        {
            if(_isDying)
            {
                return;
            }
            
            _isDying = true;
            _movement.StopMove();
            _agent.enabled = false;
            
            AnimationAnimationController.Death();
        }
        
        private void MoveAndRotate()
        {
            if (_player == null)
                return;

            if (!_agent.isActiveAndEnabled)
            {
                return;
            }

            _rotation.RotateTowards(_player.transform.position);

            if (!_spawnCompleted || AnimationAnimationController.IsAttacking)
            {
                _movement.StopMove();
                return;
            }
            
            Vector3 targetPos = (_data.BaseAttackType.Type == AttackType.Hybrid) ? _player.transform.position : (_targetPosition == Vector3.zero ? _player.transform.position : _targetPosition);
            
            if (_agent.isActiveAndEnabled)
            {
                _movement.Move(targetPos);
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