using UnityEngine;

namespace Exploring.TimescaleWithPhysics
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderBoundsTesting : MonoBehaviour
    {
        [SerializeField] private Transform _targetPoint;

        private void OnDrawGizmos()
        {
            if (_targetPoint != null) 
                GetComponent<Collider2D>().bounds.ClosestPoint(_targetPoint.position);
        }
    }
}

