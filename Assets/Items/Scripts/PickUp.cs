using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] PowerUp powerUp;

    private void OnTriggerEnter(Collider other)
    {
        IVisitable[] visitables;
        visitables = other.gameObject.GetComponentsInChildren<IVisitable>();
        if(visitables == null)
        {
            return;
        }
        foreach(IVisitable visitable in visitables)
        {
            visitable.Accept(powerUp);
        }
        Destroy(gameObject);
    }

}
