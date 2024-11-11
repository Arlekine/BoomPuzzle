using System.Collections.Generic;
using Model.NonMomentalExplosion;
using UnityEngine;

namespace Model
{
    public class ExplosionFXCreator
    {
        private ExplosionConfig _explosionConfig;
        private ExplosionFX _explosionFX;
        private Transform _fxParent;

        private List<IExplosionEventCaster> _currentEventCasters = new List<IExplosionEventCaster>();

        public ExplosionFXCreator(ExplosionConfig explosionConfig, ExplosionFX explosionFx, Transform fxParent)
        {
            _explosionConfig = explosionConfig;
            _explosionFX = explosionFx;
            _fxParent = fxParent;
        }

        public void ClearCurrent()
        {
            _currentEventCasters.ForEach(x => x.Exploded -= OnExplosionEvent);
        }

        public void SetCasters(IEnumerable<IExplosionEventCaster> casters)
        {
            foreach (var caster in casters)
            {
                caster.Exploded += OnExplosionEvent;
            }

            _currentEventCasters.AddRange(casters);
        }

        private void OnExplosionEvent(Vector2 explosionPoint)
        {
            var fx = Object.Instantiate(_explosionFX, _fxParent);
            fx.transform.position = explosionPoint;
            fx.SetSize(_explosionConfig.Radius);
        }
    }
}