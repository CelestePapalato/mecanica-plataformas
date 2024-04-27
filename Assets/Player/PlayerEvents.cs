using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IItem item = other.gameObject.GetComponent<IItem>();
        if (item != null)
        {
            item.grab();
            return;
        }
    }
}
