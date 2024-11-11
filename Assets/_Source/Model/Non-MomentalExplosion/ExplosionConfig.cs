using UnityEngine;

namespace Model.NonMomentalExplosion
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/ExplosionConfig", fileName = "ExplosionConfig")]
    public class ExplosionConfig : ScriptableObject
    {
        [SerializeField] private float _force;
        [SerializeField] private float _radius;

        public float Force => _force;
        public float Radius => _radius;
    }
}