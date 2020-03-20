using UnityEngine;

namespace TorqueGamesCore.Utilities
{
    public static class Maths
    {
        public static Plane ForwardPlane(this Transform tr)
        {
            return new Plane(tr.position, tr.forward);
        }
        public static Plane RightPlane(this Transform tr)
        {
            return new Plane(tr.position, tr.right);
        }
        public static Plane UpPlane(this Transform tr)
        {
            return new Plane(tr.position, tr.up);
        }

        public static bool IsPointFarThanDistance(this Transform tr, Vector3 point, float distance)
        {
            return (tr.position - point).sqrMagnitude > distance * distance;
        }

        public static T GetRandom<T>(this T[] array)
        {
            if (array == null || array.Length <= 0) return default;
            return array[Random.Range(0, array.Length)];
        }

        public static Vector2 Sign(this Vector2 v)
        {
            return new Vector2(Mathf.Sign(v.x),Mathf.Sign(v.y));
        }
        public static Vector2 Abs(this Vector2 v)
        {
            return new Vector2(Mathf.Abs(v.x),Mathf.Abs(v.y));
        }

        public static float Arrive(float current, float target, float step) // step = deltaTime * speed
        {
            if (Mathf.Abs(current - target) < step)
            {
                return target;
            }
            return Mathf.Sign(target - current) * step + current;
        }
        public static Vector3 Arrive(Vector3 current, Vector3 target, float step) // step = deltaTime * velocity
        {
            if ((target - current).sqrMagnitude < step * step)
            {
                return target;
            }
            
            var dir = (target - current).normalized;

            return dir * step + current;
        }
        
        
    }
}