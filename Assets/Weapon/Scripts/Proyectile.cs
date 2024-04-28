using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float explosionForce;
    [SerializeField] float radiusOfExplosion;
    [SerializeField] float timeAlive;

    private void Start()
    {
        Destroy(gameObject, timeAlive);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, radiusOfExplosion);
        foreach(Collider col in collisions)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            IDamageable damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radiusOfExplosion);
            }
        }
        Destroy(gameObject);
    }
}
