using System;
using UnityEngine;

namespace Utilities
{
    internal class CollisionListener : CollisionListenerBase
    {
        public override event Action<Collision> Enter;
        public override event Action<Collision> Stay;
        public override event Action<Collision> Exit;
        
        private void OnCollisionEnter(Collision other)
        {
            Enter?.Invoke(other);
        }

        private void OnCollisionExit(Collision other)
        {
            Exit?.Invoke(other);
        }

        private void OnCollisionStay(Collision other)
        {
            Stay?.Invoke(other);
        }
    }
}