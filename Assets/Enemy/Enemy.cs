using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _pathUpdateRate;
    [SerializeField] int _damage;

    NavMeshAgent _agent;
    HealthComponent _healthComponent;
    Transform _player;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.HealthUpdate += Damaged;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Start()
    {
        InvokeRepeating(nameof(updatePath), .2f, _pathUpdateRate);
    }

    void updatePath()
    {
        _agent.destination = _player.position;
    }

    void Damaged(int health)
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform == _player)
        {
            DamageOtherObject(collision.gameObject);
        }
    }

    void DamageOtherObject(GameObject toDamage)
    {
        IDamageable damageable = toDamage.GetComponent<IDamageable>();
        damageable.Damage(_damage);
    }

}
