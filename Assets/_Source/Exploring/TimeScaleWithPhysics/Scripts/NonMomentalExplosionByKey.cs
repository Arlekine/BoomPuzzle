using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Model.NonMomentalExplosion;
using Model.SlowMotionSystem;
using UnityEngine;

namespace Exploring.TimescaleWithPhysics
{
    public class NonMomentalExplosionByKey : MonoBehaviour, ISlowable
    {
        [SerializeField] private Transform _explosionPoint;
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private float _explosionRadius = 1f;
        [SerializeField] private float _explosionForce = 100f;
        [SerializeField] private float _explosionTime = 0.3f;

        private NonMomentalExplosion _explosion;

        public float CurrentNormalizedSpeed { get; private set; } = 1;
        public void SetNormalizedSpeed(float speed)
        {
            CurrentNormalizedSpeed = speed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var affectables = Physics2DExtensions.OverlapCircleAll<IExplosionAffectable>(_explosionPoint.position, _explosionRadius);

                foreach (var affectable in affectables)
                {
                    affectable.Affect(_explosionPoint.position, _explosionForce, _explosionRadius);
                }

                _explosion = new NonMomentalExplosion(new ExplosionData(_explosionPoint.position, _explosionRadius, float.Epsilon), new AnimationCurveAdapter(_animationCurve), _explosionTime, new MonoBehaviourCoroutineHandler(this));
                _explosion.OnBodiesAffected += body =>
                {
                    foreach (var affectable in body)
                    {
                        if (affectable is ExplosionRigidbody rigidbody)
                            print(rigidbody.gameObject.name);
                    }
                };
            }
        }

        private void OnDrawGizmos()
        {
            if (_explosionPoint == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_explosionPoint.position, _explosionRadius);


            if (_explosion == null)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_explosionPoint.position, _explosion.CurrentRadius);
        }
    }
}