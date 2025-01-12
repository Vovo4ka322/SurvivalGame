using UnityEngine;

public class CalculationFinalValue : MonoBehaviour
{
    [SerializeField] private BuffImprovment _buffImprovment;

    private Skin _skin;

    public Skin Skin => _skin;

    public void Init(Skin skin)
    {
        _skin = skin;
    }

    public float CalculateHealth() => _buffImprovment.HealthBuff.Value + _skin.Personage.Health;

    public float CalculateArmor() => _buffImprovment.ArmorBuff.Value + _skin.Personage.Armor;

    public float CalculateDamage() => _buffImprovment.DamageBuff.Value + _skin.Personage.Damage;

    public float CalculateMovementSpeed() => _buffImprovment.MovementSpeedBuff.Value + _skin.Personage.MovementSpeed;

    public float CalculateAttackSpeed() => _buffImprovment.AttackSpeedBuff.Value + _skin.Personage.AttackSpeed;
}