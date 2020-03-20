using System;
using UnityEngine;

namespace Utilities
{
    public class TransformListener : MonoBehaviour
    {
        public event Action ParentChanged;
        public event Action ChildrenChanged;
        
        private void OnTransformParentChanged()
        {
            ParentChanged?.Invoke();
        }

        private void OnTransformChildrenChanged()
        {
            ChildrenChanged?.Invoke();
        }
    }
}