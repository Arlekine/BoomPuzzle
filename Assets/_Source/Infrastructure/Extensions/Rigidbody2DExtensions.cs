using UnityEngine;

namespace Infrastructure
{
    public static class Rigidbody2DExtensions
    {
        public static void AddExplosionForce(this Rigidbody2D rigidbody, Vector2 explosionPosition,
            float explosionForce, float explosionRadius, float upwardsModifier = 0.0F,
            ForceMode2D mode = ForceMode2D.Force)
        {
            var explosionDirection = rigidbody.position - explosionPosition;
            var explosionDistance = (explosionDirection.magnitude / explosionRadius);

            if (upwardsModifier != 0)
                explosionDirection.y += upwardsModifier;

            explosionDirection.Normalize();

            rigidbody.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDirection, mode);
        }
    }
}
