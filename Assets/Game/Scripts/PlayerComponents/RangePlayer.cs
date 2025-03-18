using UnityEngine;
using Game.Scripts.AbilityComponents.ArcherAbilities.BlurAbility;
using Game.Scripts.Interfaces;
using Weapons.RangedWeapon;

namespace Game.Scripts.PlayerComponents
{
    public class RangePlayer : Player, IVampirismable, IEvasionable, IDamagable, IEnemyHitHandler
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Bow _bow;

        private float _evasionChance;

        public float Damage { get; private set; }

        public float Coefficient { get; private set; }

        public bool IsWorking { get; private set; }

        private void Start()
        {
            ChangeAttackAnimationSpeed(AnimatorState.Speed, GeneralAttackSpeed);
            Damage = GeneralDamage + _bow.BowData.Damage;
            _bow.SetHandler(this);
        }

        public void TakeDamage(float value)
        {
            if (!TryDodge())
                LoseHealth(value);

            if (Health.IsDead)
                Destroy(gameObject);
        }

        public void Shoot()
        {
            SoundCollection?.RangedPlayerSoundEffects.PlayShoot();
            _bow.StartShoot(Damage);
        }
        
        public void Reload()
        {
            SoundCollection?.RangedPlayerSoundEffects.PlayReload();
        }
        
        public float SetEvasion(Blur blur) => _evasionChance = blur.Evasion;
        
        public bool TryDodge() => Random.value <= _evasionChance;
        
        public void SetCoefficient(float value) => Coefficient = value;
        
        public bool SetTrueVampirismState() => IsWorking = true;
        
        public bool SetFalseVampirismState() => IsWorking = false;
        
        public void OnHealthRestored()
        {
            SoundCollection?.RangedPlayerSoundEffects.PlayHit();
            
            if (IsWorking)
            {
                AddHealth(Damage * Coefficient);
                Debug.Log(Damage * Coefficient + " Damage * Coefficient");
            }
        }
    }
}

public interface IEnemyHitHandler
{
    public void OnHealthRestored();
}