using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp Data", menuName = "ScriptableObjects/PowerUp Data", order = 2)]
public class PowerUp : ScriptableObject, IVisitor
{
    [SerializeField] private int HealthBonus;
    [SerializeField] private float InvincibilityTime;
    [SerializeField] private float WeaponRateMultiplier;
    [SerializeField] private float WeaponPowerUpTime;

    public void Visit(object o)
    {
        HealthComponent healthComponent = (HealthComponent)o;
        if(healthComponent && HealthBonus > 0)
        {
            healthComponent.Heal(HealthBonus);
        }
        if(healthComponent && InvincibilityTime > 0)
        {
            healthComponent.StartInvincibility(InvincibilityTime);
        }
        if(o is Weapon && WeaponRateMultiplier > 1)
        {
            Weapon weapon = o as Weapon;
        }
    }
}
