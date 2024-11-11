using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Model.NonMomentalExplosion;
using UnityEngine;

namespace Model
{
    public class ExplosionCreator
    {
        private ExplosionConfig _config;

        private List<IExplosionEventCaster> _currentEventCasters = new List<IExplosionEventCaster>();

        public ExplosionCreator(ExplosionConfig config)
        {
            _config = config;
        }

        public void ClearCurrent()
        {
            _currentEventCasters.ForEach(x =>
            {
                if (x != null)
                    x.Exploded -= OnExplosionEvent;
            });
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
            var affectables = Physics2DExtensions.OverlapCircleAll<IExplosionAffectable>(explosionPoint, _config.Radius);

            foreach (var affectable in affectables)
            {
                affectable.Affect(explosionPoint, _config.Force, _config.Radius);
            }
        }
    }
}