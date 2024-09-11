using UnityEngine;

namespace Model.NonMomentalExplosion
{
    public struct ExplosionData
    {
        private Vector3 _point;
        private float _radius;
        private float _force;

        public ExplosionData(Vector3 point, float radius, float force)
        {
            _point = point;
            _radius = radius;
            _force = force;
        }

        public Vector3 Point => _point;
        public float Radius => _radius;
        public float Force => _force;
    }
}