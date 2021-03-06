using System;
using UnityEngine;

namespace Utilities
{
    public class TriggerListener : MonoBehaviour
    {
        public event Action<Collider> Enter;
        public event Action<Collider> Stay;
        public event Action<Collider> Exit;
        
        private void OnTriggerEnter(Collider other)
        {
            Enter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Exit?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            Stay?.Invoke(other);
        }
    }
}