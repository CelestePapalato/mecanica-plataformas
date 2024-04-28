using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    public UnityEvent NoHealth;
    private bool invincibility = false;

    private int _health;
    void Awake()
    {
        _health = _maxHealth;
    }

    public void Damage(int value)
    {
        if (invincibility)
        {
            return;
        }
        _health = Mathf.Clamp(_health - value, 0, _maxHealth);
        if (_health == 0)
        {
            NoHealth.Invoke();
        }
    }

    public void Heal(int value)
    {
        _health = Mathf.Clamp(_health + value, 0, _maxHealth);
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
