using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPlatform : MonoBehaviour
{
    [SerializeField] float bounceImpulse;
    [SerializeField][Range(0,50)] float heightOffset;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb;
        Transform objectTransform = collision.transform;
        float positionY = transform.position.y + (heightOffset * transform.localScale.y / 100);
        if (positionY > objectTransform.position.y)
        {
            return;
        }
        if(collision.gameObject.TryGetComponent<Rigidbody>(out rb))
        {
            rb.AddForce(collision.gameObject.transform.up * bounceImpulse, ForceMode.Impulse);
        }
    }
}
