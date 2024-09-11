using System.Collections.Generic;
using Infrastructure;
using Model.NonMomentalExplosion;
using UnityEngine;

namespace Exploring.TimescaleWithPhysics
{
    public class NonMomentalExplosionByKey : MonoBehaviour
    {
        [SerializeField] private Transform _explosionPoint;
        [SerializeField] private float _explosionRadius = 1f;
        [SerializeField] private float _explosionForce = 100f;
        [SerializeField] private float _explosionTime = 0.3f;

        private NonMomentalExplosion _explosion;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _explosion = new NonMomentalExplosion(new ExplosionData(_explosionPoint.position, _explosionRadius, _explosionForce), 0.1f, _explosionTime, new MonoBehaviourCoroutineHandler(this));
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