using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _pathUpdateRate;
    [SerializeField] int _damage;
    [SerializeField] bool _pushObjectsWhenDamaged = false;
    [SerializeField] float _pushImpulseValue;
    [SerializeField] LayerMask _floorLayerMask;
    [SerializeField][Range(0f, 0.1f)] float _floorRaycastLength;

    NavMeshAgent _agent;
    HealthComponent _healthComponent;
    Transform _player;
    Rigidbody _rb;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.HealthUpdate += Damaged;
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        DetectionArea detectionArea = GetComponentInChildren<DetectionArea>();
        detectionArea.Entered += FollowPlayer;
        FollowPlayer(false);
    }

    private void Update()
    {
        Vector3 rotationOG = transform.rotation.eulerAngles;
        transform.LookAt(_player);
        Vector3 newRotation = transform.rotation.eulerAngles;
        newRotation.x = rotationOG.x;
        newRotation.z = rotationOG.z;
        transform.rotation = Quaternion.Euler(newRotation);
    }

    void updatePath()
    {
        _agent.destination = _player.position;
        if (_agent.pathStatus == NavMeshPathStatus.PathInvalid && isOnFloor())
        {
            Debug.Log("Rigidbody movement " + name);
            Vector3 playerDirection = _player.position - transform.position;
            playerDirection.y = 0;
            playerDirection = playerDirection.normalized;
            _rb.AddForce(playerDirection * _agent.speed, ForceMode.VelocityChange);
            return;
        }
        _rb.velocity = Vector3.zero;
    }

    private void FollowPlayer(bool state)
    {
        if (state)
        {
            _agent.enabled = true;
            InvokeRepeating(nameof(updatePath), .2f, _pathUpdateRate);
        }
        else
        {
            CancelInvoke(nameof(updatePath));
            _agent.enabled = false;
            Vector3 velocity = _rb.velocity;
            velocity.x = 0;
            velocity.z = 0;
            _rb.velocity = velocity;
        }
    }

    void Damaged(int health)
    {
        if (health <= 0)
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
        if (_pushObjectsWhenDamaged && _pushImpulseValue > 0)
        {
            Rigidbody enemyRB = toDamage.GetComponent<Rigidbody>();
            Vector3 impulseVector = toDamage.transform.position - transform.position;
            impulseVector = impulseVector.normalized;
            enemyRB.AddForce(impulseVector * _pushImpulseValue, ForceMode.Impulse);
        }
    }

    private bool isOnFloor()
    {
        float altura = transform.lossyScale.y;
        return Physics.Raycast(transform.position, -transform.up, altura / 2 + _floorRaycastLength, _floorLayerMask);
    }
}