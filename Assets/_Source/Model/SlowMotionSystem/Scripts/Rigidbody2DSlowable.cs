using DG.Tweening;
using UnityEngine;

namespace Model.SlowMotionSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DSlowable : MonoBehaviour, ISlowable
    {
        private Rigidbody2D _rigidbody;
        private float _normalizedSpeed = 1f;

        public Rigidbody2D Rigidbody2D
        {
            get
            {
                if (_rigidbody == null)
                    _rigidbody = GetComponent<Rigidbody2D>();

                return _rigidbody;
            }
        }

        public float CurrentNormalizedSpeed
        {
            get => _normalizedSpeed;
            private set
            {
                Rigidbody2D.velocity *= value / _normalizedSpeed;
                Rigidbody2D.angularVelocity *= value / _normalizedSpeed;
                Rigidbody2D.gravityScale *= (value * value) / _normalizedSpeed;

                Rigidbody2D.drag *= value / _normalizedSpeed;
                Rigidbody2D.angularDrag *= value / _normalizedSpeed;

                _normalizedSpeed = value;
            }
        }

        public void SetNormalizedSpeed(float speed)
        {
            CurrentNormalizedSpeed = speed;
        }
    }
}