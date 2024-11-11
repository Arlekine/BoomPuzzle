using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure
{
    public static class Physics2DExtensions
    {
        public static List<T> OverlapCircleAll<T>(Vector3 position, float radius, bool includeComponentsFromSameObject = false)
        {
            var colliders = Physics2D.OverlapCircleAll(position, radius);
            var objects = new List<T>();

            foreach (var col in colliders)
            {
                if (includeComponentsFromSameObject)
                {
                    if (col.TryGetComponent(out T component))
                        objects.Add(component);
                }
                else
                {
                    var affectables = col.GetComponents<T>();
                    if (affectables.Any())
                        objects.AddRange(affectables);
                }
            }

            return objects;
        }
    }
}