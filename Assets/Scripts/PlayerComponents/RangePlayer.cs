using Ability.ArcherAbilities.Blur;
using EnemyComponents;
using PlayerComponents;
using UnityEngine;
using Weapons.RangedWeapon;

public class RangePlayer : Player, IVampirismable, IEvasionable
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Bow _bow;

    private float _evasionChance;

    public float Damage { get; private set; }

    public float Coefficient { get; private set; }

    public bool IsWorking { get; private set; }

    public Bow Bow => _bow;

    private void OnEnable()
    {
        _bow.ArrowTouched += OnHealthRestored;
    }

    private void OnDisable()
    {
        _bow.ArrowTouched -= OnHealthRestored;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (TryDodge())
            {
                Physics.IgnoreCollision(_collider, enemy.Collider);
            }
            else
            {
                LoseHealth(enemy.SetDamage());
            }

            if (Health.IsDead)
            {
                Destroy(gameObject);
            }
        }
    }


    public void Shoot()
    {
        _bow.StartShoot();
    }

    public float SetEvasion(Blur blur) => _evasionChance = blur.Evasion;

    public bool TryDodge() => Random.value <= _evasionChance;

    public void SetCoefficient(float value) => Coefficient = value;

    public bool SetTrueVampirismState() => IsWorking = true;

    public bool SetFalseVampirismState() => IsWorking = false;

    private void OnHealthRestored()
    {
        if (IsWorking)
        {
            AddHealth(Damage * Coefficient);
        }
    }
}
