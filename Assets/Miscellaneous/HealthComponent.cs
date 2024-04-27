using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    public UnityEvent NoHealth;

    private int _health;
    void Awake()
    {
        _health = _maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        _health = Mathf.Clamp(_health--, 0, _maxHealth);
        if(_health == 0)
        {
            NoHealth.Invoke();
        }
    }

    public void Heal(int value)
    {
        _health = Mathf.Clamp(_health + value, 0, _maxHealth);
    }
}
