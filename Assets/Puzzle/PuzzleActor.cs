using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PuzzleSystem
{
    public abstract class PuzzleActor : MonoBehaviour, IInteractable
    {
        private bool _completed = false;
        public bool Completed
        {
            get { return _completed; }
            protected set
            {
                if (_completed != value)
                {
                    _completed = value;
                    PuzzleStateUpdated.Invoke();
                }
            }
        }

        public UnityAction PuzzleStateUpdated;

        public abstract void Interact();
        public float DistanceTo(Vector3 point)
        {
            return Vector3.Distance(transform.position, point);
        }
    }
}