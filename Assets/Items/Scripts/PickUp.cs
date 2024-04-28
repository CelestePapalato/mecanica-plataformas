using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] PowerUp powerUp;

    private void OnTriggerEnter(Collider other)
    {
        IVisitable visitable;
        if (TryGetComponent<IVisitable>(out visitable))
        {
            visitable.Accept(powerUp);
            Destroy(gameObject);
        }
    }

}
