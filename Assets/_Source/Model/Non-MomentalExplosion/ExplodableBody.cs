using System;
using Model.ClickSystem;
using UnityEngine;

namespace Model.NonMomentalExplosion
{
    public class ExplodableBody : MonoBehaviour, IExplosionAffectable, IExplosionEventCaster, IClickable
    {
        [SerializeField] private bool _oneUse = true;

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

                if (_oneUse)
                    Destroy(gameObject);
                
                Exploded?.Invoke(transform.position);
            }

            if (_oneUse == false)
                _exploded = false;
        }
    }
}