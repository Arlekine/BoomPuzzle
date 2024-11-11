using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class ExplosionFX : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _fxToChangeSpawnSize;
        [SerializeField] private List<ParticleSystem> _fxToChangeShapeRadius;
        [SerializeField] private float _sizeRadiusMultiplayer = 1f;
        [SerializeField] private float _shapeRadiusMultiplayer = 1f;

        public void SetSize(float size)
        {
            transform.localScale = size * Vector3.one;
        }

        private void SetSpawnSize(ParticleSystem system, float targetSize)
        {
            var main = system.main;
            main.startSizeMultiplier = targetSize * _sizeRadiusMultiplayer;
        }

        private void SetShapeRadius(ParticleSystem system, float targetSize)
        {
            var main = system.shape;
            main.radius = targetSize * _shapeRadiusMultiplayer;
        }
    }
}