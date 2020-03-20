using System;
using UnityEngine;

namespace Utilities
{
    public class CollisionListenerComposite : CollisionListenerBase
    {
        public CollisionListenerBase[] Listeners { get; set; }

        public override event Action<Collision> Enter
        {
            add
            {
                foreach (var listener in Listeners)
                {
                    listener.Enter += value;
                }
            }
            remove
            {
                foreach (var listener in Listeners)
                {
                    listener.Enter -= value;
                }
            }
        }
        public override event Action<Collision> Stay
        {
            add
            {
                foreach (var listener in Listeners)
                {
                    listener.Stay += value;
                }
            }
            remove
            {
                foreach (var listener in Listeners)
                {
                    listener.Stay -= value;
                }
            }
        }
        public override event Action<Collision> Exit
        {
            add
            {
                foreach (var listener in Listeners)
                {
                    listener.Exit += value;
                }
            }
            remove
            {
                foreach (var listener in Listeners)
                {
                    listener.Exit -= value;
                }
            }
        }
    }
}