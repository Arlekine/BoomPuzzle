using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

namespace Exploring.TimescaleWithPhysics
{
    public class SimpleExplosionByKey : MonoBehaviour
    {
        [SerializeField] private Transform _explosionPoint;
        [SerializeField] private float _explosionRadius = 1f;
        [SerializeField] private float _explosionForce = 100f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var colliders = Physics2D.OverlapCircleAll(_explosionPoint.position, _explosionRadius);
                var rigidbodies = new List<Rigidbody2D>();

                foreach (var col in colliders)
                {
                    if (col.TryGetComponent<Rigidbody2D>(out Rigidbody2D body))
                        rigidbodies.Add(body);
                }

                foreach (var body in rigidbodies)
                {
                    body.AddExplosionForce(_explosionPoint.position, _explosionForce, _explosionRadius);
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_explosionPoint == null)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_explosionPoint.position, _explosionRadius);
        }
    }
}