using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PuzzleSystem
{
    public abstract class PuzzleActor : MonoBehaviour
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

        private void OnMouseDown()
        {
            Debug.Log(name + " interacted");
            OnInteraction();
        }

        protected abstract void OnInteraction();

    }
}