using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using UnityEngine;

namespace Model.NonMomentalExplosion
{
    public class NonMomentalExplosion : IDisposable
    {
        private ExplosionData _data;
        private float _time;

        private ICoroutineHandler _coroutineHandler;

        public NonMomentalExplosion(ExplosionData data, float initialRadius, float time, ICoroutineHandler coroutineHandler)
        {
            if (initialRadius >= data.Radius)
                throw new ArgumentException($"{nameof(initialRadius)} should be less than final radius");

            _data = data;
            _time = time;
            _coroutineHandler = coroutineHandler;

            _currentRadius = initialRadius;

            _coroutineHandler.StartCoroutine(ExplosionRoutine());
        }

        private float _currentRadius;
        private Coroutine _currentExplosionRoutine;
        private List<IExplosionAffectable> _affectedBodies = new List<IExplosionAffectable>();

        public event Action<IEnumerable<IExplosionAffectable>> OnBodiesAffected;
        public event Action Completed;

        public float CurrentRadius => _currentRadius;

        private IEnumerator ExplosionRoutine()
        {
            var currentTime = 0f;
            while (currentTime <= _time)
            {
                var affectables = Physics2DExtensions.OverlapCircleAll<IExplosionAffectable>(_data.Point, _currentRadius);
                affectables = affectables.Where(x => _affectedBodies.Contains(x) == false).ToList();

                foreach (var affectable in affectables)
                {
                    affectable.Affect(_data.Point, _data.Force, _data.Radius);
                    _affectedBodies.Add(affectable);
                }

                if (affectables.Count > 0 )
                    OnBodiesAffected?.Invoke(affectables);
                
                yield return null;

                currentTime += Time.deltaTime;

                var currentProgress = Mathf.InverseLerp(0f, _time, currentTime);
                _currentRadius = currentProgress * _data.Radius;
            }

            Completed?.Invoke();
        }

        public void Dispose()
        {
            if (_currentExplosionRoutine != null)
                _coroutineHandler.StopCoroutine(_currentExplosionRoutine);
        }
    }
}
