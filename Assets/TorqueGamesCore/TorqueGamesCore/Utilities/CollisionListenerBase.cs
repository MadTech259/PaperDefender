using System;
using UnityEngine;

namespace Utilities
{
    public abstract class CollisionListenerBase : MonoBehaviour
    {
        public abstract event Action<Collision> Enter;
        public abstract event Action<Collision> Stay;
        public abstract event Action<Collision> Exit;
    }
}