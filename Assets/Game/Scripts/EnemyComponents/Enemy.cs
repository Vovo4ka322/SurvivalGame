using Game.Scripts.EnemyComponents.Animations;
using Game.Scripts.EnemyComponents.EnemySettings;
using Game.Scripts.EnemyComponents.EnemySettings.Effects;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyAttack.EnemyAttackType;
using Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors;
using Game.Scripts.EnemyComponents.Interfaces;
using Game.Scripts.MusicComponents.EffectSounds;
using Game.Scripts.PoolComponents;
using Game.Scripts.PlayerComponents;
using Game.Scripts.ProjectileComponents.CreateProjectiles;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.EnemyComponents
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyEffects))]
    public class Enemy : Character
    {
        [SerializeField] private EnemyData _data;

        private Player _playerTransform;
        private Vector3 _targetPosition;
        private Vector3 _lockedTargetPosition = Vector3.zero;

        private Animator _animator;
        private Coroutine _movementCoroutine;
        private NavMeshAgent _agent;
        private EnemyAnimationState _animationState;
        private EnemyAttackExecutor _attackExecutor;
        private SoundCollection _soundCollection;

        private IAttackBehavior _attackBehavior;
        private IEnemyMovement _movement;
        private IEnemyRotation _rotation;
        private IEnemyAttack _enemyAttack;
        private IEnemyEffects _enemyEffects;
        private ICoroutineRunner _coroutineRunner;

        private BaseProjectileSpawner _projectileSpawner;
        private RangedProjectileSpawner _rangedSpawner;
        private HybridProjectileSpawner _hybridSpawner;

        private bool _spawnCompleted = false;
        private bool _isDying = false;

        public event Action<Enemy> Dead;
        public event Action<Enemy> Enabled;
        public event Action<float> Changed;

        public EnemyData Data => _data;
        public Player PlayerTransform => _playerTransform;
        public EnemyAnimationState AnimationAnimationState => _animationState;
        public SoundCollection SoundCollection => _soundCollection;
        public IEnemyAttack EnemyAttack => _enemyAttack;
        public IEnemyMovement Movement => _movement;
        public IAttackBehavior AttackBehavior => _attackBehavior;
        public bool SpawnCompleted => _spawnCompleted;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemyEffects = GetComponent<EnemyEffects>();
            _animationState = new EnemyAnimationState(_animator, _data.EnemyType);
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
                _movementCoroutine = _coroutineRunner.StartCoroutine(_attackExecutor.AttackCoroutine());
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
            _playerTransform = player;
            _data = enemyData;
            _coroutineRunner = coroutineRunner;

            _animationState = new EnemyAnimationState(_animator, _data.EnemyType);
            _movement = new EnemyMover(AnimationAnimationState, _agent, _data.MoveSpeed);
            _rotation = new EnemyRotation(transform, _data.RotationSpeed);

            _enemyEffects.Initialize(_data, pool, coroutineRunner);

            if (_data.BaseAttackType.Type == AttackType.Ranged)
            {
                _projectileSpawner = _rangedSpawner;
            }
            else if (_data.BaseAttackType.Type == AttackType.Hybrid)
            {
                _projectileSpawner = _hybridSpawner;
            }

            _projectileSpawner?.Initialize(_data, _playerTransform, poolManager);

            _enemyAttack = new EnemySettings.EnemyAttack.EnemyAttack(AnimationAnimationState, transform, _playerTransform, _data.AttackCooldown, _data.BaseAttackType, AnimationAnimationState.AttackVariantsCount);
            _attackExecutor = new EnemyAttackExecutor(this);

            _attackExecutor.SetAttackBehavior();

            _targetPosition = _playerTransform.transform.position;

            AnimationAnimationState.Spawn();
            Health.InitMaxValue(_data.MaxHealth);

            if (_data.EnemyType == EnemyType.Boss)
            {
                SpawnAnimationEnd();
            }

            if (_coroutineRunner != null && _movementCoroutine == null && gameObject.activeInHierarchy)
            {
                _movementCoroutine = _coroutineRunner.StartCoroutine(_attackExecutor.AttackCoroutine());
            }
        }

        public void SetSoundCollection(SoundCollection soundCollection)
        {
            _soundCollection = soundCollection;

            if (_rangedSpawner != null)
            {
                _rangedSpawner.SetSound(soundCollection);
            }

            if (_hybridSpawner != null)
            {
                _hybridSpawner.SetSound(soundCollection);
            }
        }

        public void ChangeHealth(float value)
        {
            Health.Lose(value);

            Changed?.Invoke(Health.Value);
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

        public float GetDamage()
        {
            return Data.Damage;
        }

        public void SpawnAnimationEnd()
        {
            _spawnCompleted = true;
        }

        public void AttackAnimationEnd()
        {
            AnimationAnimationState?.ResetAttackState();

            if (_data.EnemyType == EnemyType.Boss)
            {
                ClearLockedTargetPosition();
            }
        }

        public void LockTargetPosition(Vector3 position)
        {
            _lockedTargetPosition = position;
        }

        public void ClearLockedTargetPosition()
        {
            _lockedTargetPosition = Vector3.zero;
        }

        public void OnDeathAnimationEvent()
        {
            _enemyEffects.Death();
        }

        public void DeathAnimationEnd()
        {
            if (_playerTransform != null)
            {
                _playerTransform.GetExperience(_data.Experience);
                _playerTransform.GetMoney(_data.Money);
                _playerTransform.AddPoints(_data.Points);
            }

            Dead?.Invoke(this);
        }

        public void ResetState()
        {
            _isDying = false;
            _spawnCompleted = false;
        }

        internal void SetAttackBehaviorInternal(IAttackBehavior attackBehavior)
        {
            _attackBehavior = attackBehavior;
        }

        private void OnDead()
        {
            if (_isDying)
            {
                return;
            }

            _isDying = true;
            _hybridSpawner?.CancelPreparedProjectile();
            _enemyEffects.StopSpawn();
            _coroutineRunner.StopCoroutine(_movementCoroutine);
            _movementCoroutine = null;
            _movement.Stop();
            _agent.enabled = false;

            AnimationAnimationState.Death();
        }

        private void MoveAndRotate()
        {
            if (_playerTransform == null || Health.IsDead)
            {
                return;
            }

            if (!_agent.isActiveAndEnabled)
            {
                return;
            }

            if (_data.EnemyType != EnemyType.Boss || _lockedTargetPosition == Vector3.zero)
            {
                _rotation.RotateTowards(_playerTransform.transform.position);
            }

            if (!_spawnCompleted || AnimationAnimationState.IsAttacking)
            {
                _movement.Stop();
                return;
            }

            Vector3 targetPosition;

            if (_data.BaseAttackType.Type == AttackType.Boss)
            {
                targetPosition = (_lockedTargetPosition != Vector3.zero) ? _lockedTargetPosition : _playerTransform.transform.position;
            }
            else if (_data.BaseAttackType.Type == AttackType.Hybrid)
            {
                targetPosition = _playerTransform.transform.position;
            }
            else
            {
                targetPosition = (_targetPosition == Vector3.zero) ? _playerTransform.transform.position : _targetPosition;
            }

            if (_agent.isActiveAndEnabled)
            {
                _movement.ProcessMovement(targetPosition, _spawnCompleted, AnimationAnimationState.IsAttacking);
                _movement.StartMoving();
            }
            else
            {
                _movement.Stop();
            }
        }
    }
}