using UnityEngine;

namespace Model.NonMomentalExplosion
{
    public interface IExplosionAffectable
    {
        void Affect(Vector2 explosionPosition, float explosionForce, float explosionRadius);
    }
}