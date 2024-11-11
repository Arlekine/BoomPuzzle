using System;
using Model.ClickSystem;
using UnityEngine;

namespace Model.NonMomentalExplosion
{
    public class ExplodableBody : MonoBehaviour, IExplosionAffectable, IExplosionEventCaster, IClickable
    {
        public event Action<Vector2> Exploded;

        private bool _exploded;

        public void Affect(Vector2 explosionPosition, float explosionForce, float explosionRadius)
            => Explode();

        public void Click()
            => Explode();

        private void Explode()
        {
            if (_exploded == false)
            {
                _exploded = true;
                Destroy(gameObject);
                Exploded?.Invoke(transform.position);
            }
        }
    }
}