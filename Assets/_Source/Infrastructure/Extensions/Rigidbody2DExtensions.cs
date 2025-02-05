using UnityEngine;

namespace Infrastructure
{
    public static class Rigidbody2DExtensions
    {
        public static void AddExplosionForce(this Rigidbody2D rigidbody, Vector2 explosionPosition,
            float explosionForce, float explosionRadius, float upwardsModifier = 0.0F,
            ForceMode2D mode = ForceMode2D.Impulse)
        {
            Collider2D[] colliders = new Collider2D[1];
            rigidbody.GetAttachedColliders(colliders);

            var affectionPoint = colliders.Length == 0 || colliders[0].bounds.Contains(explosionPosition) ? rigidbody.position
                : (Vector2)colliders[0].bounds.ClosestPoint(explosionPosition);

            var explosionDirection = rigidbody.position - explosionPosition;
            var explosionDistance = Mathf.Clamp01(explosionDirection.magnitude / explosionRadius);

            if (upwardsModifier != 0)
                explosionDirection.y += upwardsModifier;

            explosionDirection.Normalize();
            //rigidbody.AddForceAtPosition(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDirection, affectionPoint, mode);

            Debug.Log($"{rigidbody.gameObject.name} - {explosionDistance}");
            rigidbody.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDirection, mode);
        }
    }
}
