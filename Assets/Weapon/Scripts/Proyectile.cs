using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float radiusOfExplosion;
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, radiusOfExplosion);
        foreach(Collider col in collisions)
        {
            IDamageable damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
        }
        Destroy(gameObject);
    }
}
