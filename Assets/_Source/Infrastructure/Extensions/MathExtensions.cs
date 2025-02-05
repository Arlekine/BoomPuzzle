using UnityEngine;

namespace Infrastructure
{
    public static class MathExtensions
    {
        public static Vector2 GetClosestPointOnLine(Vector2 origin, Vector2 direction, Vector2 point)
        {
            direction.Normalize();
            Vector2 lhs = point - origin;

            float dotP = Vector2.Dot(lhs, direction);
            return origin + direction * dotP;
        }
    }
}