using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, IDamageable, IVisitable
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    public UnityEvent NoHealth;
    public UnityEvent<int> HealthUpdate;
    private bool invincibility = false;

    private int _health;
    void Awake()
    {
        _health = _maxHealth;
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
        _health = Mathf.Clamp(_health - value, 0, _maxHealth);
        HealthUpdate.Invoke(_health);
        if (_health == 0)
        {
            NoHealth.Invoke();
        }
    }

    public void Heal(int value)
    {
        _health = Mathf.Clamp(_health + value, 0, _maxHealth);
        HealthUpdate.Invoke(_health);
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
