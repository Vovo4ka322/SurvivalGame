using System;
using UnityEngine;
using EnemyComponents.Animations;
using EnemyComponents.EnemySettings;
using EnemyComponents.EnemySettings.Effects;
using EnemyComponents.EnemySettings.EnemyAttackType;
using EnemyComponents.EnemySettings.EnemyBehaviors;
using EnemyComponents.Projectiles;
using PlayerComponents;

namespace EnemyComponents
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(EnemyEffects))]
    public class Enemy : Character
    {
        [SerializeField] private EnemyData _data;
        
        private Player _player;
        private Animator _animator;
        private EnemyMovement _movement;
        private EnemyRotation _rotation;
        private EnemyAttack _enemyAttack;
        private EnemyCollider _enemyCollider;
        private EnemyEffects _enemyEffects;
        private BaseProjectileSpawner _projectileSpawner;
        private RangedProjectileSpawner _rangedSpawner;
        private HybridProjectileSpawner _hybridSpawner;
        
        public EnemyData Data => _data;
        public Collider Collider { get; private set; }
        public EnemyAnimationController AnimationController { get; private set; }

        public event Action<Enemy> Dead;
        public event Action<Enemy> Enabled;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemyEffects = GetComponent<EnemyEffects>();
            Collider = GetComponent<Collider>();
            
            _rangedSpawner = GetComponent<RangedProjectileSpawner>();
            _hybridSpawner = GetComponent<HybridProjectileSpawner>();
        }

        private void OnEnable()
        {
            if(Health != null)
            {
                Health.Death += Death;
            }
            Enabled?.Invoke(this);
        }

        private void OnDisable()
        {
            if(Health != null)
            {
                Health.Death -= Death;
            }
            
            Dead?.Invoke(this);
        }
        
        private void Update()
        {
            if(_player == null) return;
            
            float distance = Vector3.Distance(transform.position, _player.transform.position);
            
            if(distance > 50f)
            {
                SimpleBehavior();
                if(Collider.enabled) Collider.enabled = false;
            }
            else
            {
                MovementAndAttack();
                _rotation.RotateTowards(_player.transform.position);
                if(!Collider.enabled) Collider.enabled = true;
            }
        }

        public void InitializeComponents(Player player, EnemyData enemyData)
        {
            _player = player;
            _data = enemyData;
            _enemyCollider = new EnemyCollider(this, _player);
            AnimationController = new EnemyAnimationController(_animator, _data.EnemyType);
            _movement = new EnemyMovement(transform, _data.MoveSpeed, AnimationController, _data.RotationSpeed);
            _rotation = new EnemyRotation(transform, _data.RotationSpeed);
            _enemyEffects.InitializeParticle(_data);
            
            if(_data.BaseAttackType.Type == AttackType.Ranged)
            {
                _projectileSpawner = _rangedSpawner;
            }
            else if(_data.BaseAttackType.Type == AttackType.Hybrid)
            {
                _projectileSpawner = _hybridSpawner;
            }
            
            _projectileSpawner?.Initialize(_data, _player);
            
            _enemyAttack = new EnemyAttack(AnimationController, transform, _player, _data.AttackCooldown, _data.BaseAttackType, AnimationController.AttackVariantsCount);
            //Health.InitMaxValue(_data.MaxHealth);
        }
        
        private void SimpleBehavior()
        {
            if(_movement != null && _player != null)
            {
                _movement.Move(_player.transform.position);
            }
        }
        public float SetDamage()
        {
            return Data.Damage;
        }

        public void OnAttackAnimationEnd()
        {
            AnimationController?.ResetAttackState();
        }

        private void OnTriggerEnter(Collider other)
        {
            _enemyCollider.HandleCollision(other);
        }

        private void Death()
        {
            AnimationController.Death();
            Dead?.Invoke(this);
        }

        private void MovementAndAttack()
        {
            if(_player == null)
            {
                _movement.PlayMove();
                _movement.Move(transform.position);

                return;
            }

            if(AnimationController.IsAttacking)
            {
                _movement.StopMove();

                return;
            }

            float distance = Vector3.Distance(transform.position, _player.transform.position);
            AttackType currentAttackType = _data.BaseAttackType.Type;

            if(currentAttackType == AttackType.Hybrid)
            {
                HybridEnemyAttackType hybrid = (HybridEnemyAttackType)_data.BaseAttackType;

                if(distance > hybrid.RangedRange)
                {
                    _movement.PlayMove();
                    _movement.Move(_player.transform.position);
                }
                else if(distance > hybrid.MeleeRange)
                {
                    if(_enemyAttack.IsHybridProjectileReady(hybrid, distance))
                    {
                        _movement.StopMove();
                        _enemyAttack.TryAttack();
                    }
                    else
                    {
                        _movement.PlayMove();
                        _movement.Move(_player.transform.position);
                    }
                }
                else
                {
                    _movement.StopMove();
                    _enemyAttack.TryAttack();
                }
            }
            else if(currentAttackType == AttackType.Ranged)
            {
                if(distance > _data.AttackRange)
                {
                    _movement.PlayMove();
                    _movement.Move(_player.transform.position);
                }
                else
                {
                    _movement.StopMove();
                    _enemyAttack.TryAttack();
                }
            }
            else
            {
                if(distance > Data.AttackRange)
                {
                    _movement.PlayMove();
                    _movement.Move(_player.transform.position);
                }
                else
                {
                    _movement.StopMove();
                    _enemyAttack.TryAttack();
                }
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
