using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PuzzleSystem
{
    public class Puzzle : MonoBehaviour
    {
        public UnityEvent OnPuzzleComplete;

        private PuzzleActor[] _puzzleActors;

        private void Awake()
        {
            _puzzleActors = GetComponentsInChildren<PuzzleActor>();
            foreach (var puzzleActor in _puzzleActors)
            {
                puzzleActor.PuzzleStateUpdated += OnPuzzleStateUpdated;
            }
        }

        private void OnPuzzleStateUpdated()
        {
            bool completed = true;
            foreach (PuzzleActor puzzleActor in _puzzleActors)
            {
                completed = completed && puzzleActor.Completed;
            }
            if (completed)
            {
                OnPuzzleComplete.Invoke();
            }
        }
    }
}