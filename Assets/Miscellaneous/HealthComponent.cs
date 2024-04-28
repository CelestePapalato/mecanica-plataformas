using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, IDamageable, IVisitable
{
    [SerializeField]
    private int _maxHealth;
    public int MaxHealth { get { return _maxHealth; } }
    [SerializeField]
    public UnityEvent NoHealth;
    public UnityAction<int> HealthUpdate;
    private bool invincibility = false;

    private int _health;
    public int Health
    {
        get { return _health; }
        private set
        {
            _health = value;
            if(HealthUpdate != null)
            {
                HealthUpdate(_health);
            }
        }
    }

    void Awake()
    {
        Health = _maxHealth;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Damage(int value)
    {
        if (invincibility)
        {
            return;
        }
        Health = Mathf.Clamp(_health - value, 0, _maxHealth);
        if (Health == 0)
        {
            NoHealth.Invoke();
        }
    }

    public void Heal(int value)
    {
        Health = Mathf.Clamp(Health + value, 0, _maxHealth);
    }

    public void Invincibility(float time)
    {
        if (invincibility)
        {
            StopAllCoroutines();
        }
        StartCoroutine(StartInvincibility(time));
    }

    public IEnumerator StartInvincibility(float time)
    {
        invincibility = true;
        yield return new WaitForSeconds(time);
        invincibility = false;
    }

}
