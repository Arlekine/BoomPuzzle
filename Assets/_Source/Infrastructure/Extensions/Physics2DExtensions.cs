using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public static class Physics2DExtensions
    {
        public static List<T> OverlapCircleAll<T>(Vector3 position, float radius)
        {
            var colliders = Physics2D.OverlapCircleAll(position, radius);
            var objects = new List<T>();

            foreach (var col in colliders)
            {
                if (col.TryGetComponent<T>(out T body))
                    objects.Add(body);
            }

            return objects;
        }
    }
}