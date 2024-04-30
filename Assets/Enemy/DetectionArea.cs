using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionArea : MonoBehaviour
{
    public UnityAction<bool> Entered;

    private void OnTriggerEnter(Collider other)
    {
        if (Entered != null)
        {
            Entered(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Entered != null)
        {
            Entered(false);
        }
    }
}
