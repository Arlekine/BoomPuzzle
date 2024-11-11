using Infrastructure;
using UnityEngine;
using Zenject;

namespace Model.NonMomentalExplosion
{
    public class ExplosionRigidbody : MonoBehaviour, IExplosionAffectable
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        public virtual void Affect(Vector2 explosionPosition, float explosionForce, float explosionRadius) =>
            _rigidbody.AddExplosionForce(explosionPosition, explosionForce, explosionRadius);


        private void OnValidate()
        {
            if (_rigidbody == null && TryGetComponent<Rigidbody2D>(out Rigidbody2D body))
                _rigidbody = body;
        }
    }
}