using UnityEngine;

namespace Infrastructure
{
    public static class TransformExtensions
    {
        public static void DestroyAllChildrenImmediately(this Transform transform)
        {
            while (transform.childCount != 0)
                Object.DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}