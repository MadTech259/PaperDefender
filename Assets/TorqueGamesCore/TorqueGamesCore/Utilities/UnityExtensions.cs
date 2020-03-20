using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class UnityExtensions
    {

        public static TComponent GetOrCreate<TComponent>(this GameObject obj) where TComponent : Component
        {
            var component = obj.GetComponent<TComponent>();
            if (!component)
            {
                component = obj.AddComponent<TComponent>();
            }
            return component;
        }



        public static Transform GetChildByName(this Transform tr, string name)
        {
            foreach (var t in tr.GetComponentsInChildren<Transform>())
            {
                if (t.name == name) return t;
                var name2 = t.name.Replace("mixamorig1", "mixamorig");
                name = name.Replace("mixamorig1", "mixamorig");
                if (name2 == name) return t;
            }

            return null;
        }
        public static TComponent GetOrCreate<TComponent>(this Component obj) where TComponent : Component
        {
            var component = obj.GetComponent<TComponent>();
            if (!component)
            {
                component = obj.gameObject.AddComponent<TComponent>();
            }
            return component;
        }

        public static List<T> AsList<T>(this T t)
        {
            return new List<T> {t};
        }
        
    }
}