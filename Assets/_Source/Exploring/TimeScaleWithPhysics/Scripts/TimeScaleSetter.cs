using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Model.SlowMotionSystem;
using UnityEngine;

namespace Exploring.TimescaleWithPhysics
{
    public class TimeScaleSetter : MonoBehaviour
    {
        [Range(0.01f, 2f)][SerializeField] private float _timeScale = 1f;
        [SerializeField] private Transform _slowablesParent;

        private List<ISlowable> _slowables = new List<ISlowable>();
        private float _currentTimeScale = 1f;
        private float _defaultFixedDeltaTime;

        private void Start()
        {
            _defaultFixedDeltaTime = Time.fixedDeltaTime;

            _slowables = _slowablesParent.GetComponentsInChildren<ISlowable>().ToList();
            UpdateSlowables();
        }

        private void Update()
        {
            /*if (Input.GetKeyUp(KeyCode.Space))
            {
                DOTween.To(() => _timeScale, x => _timeScale = x, 0.1f, 0.5f).onComplete += () =>
                {
                    DOTween.To(() => _timeScale, x => _timeScale = x, 1f, 0.5f);
                };
            }*/
            
            /*_currentTimeScale = _timeScale;
            UpdateSlowables();*/
            _currentTimeScale = _timeScale;
            Time.timeScale = _currentTimeScale;
            Time.fixedDeltaTime = _defaultFixedDeltaTime * _currentTimeScale;
        }

        private void UpdateSlowables()
        {
            foreach (var slowable in _slowables)
            {
                slowable.SetNormalizedSpeed(_currentTimeScale);
            }
        }
    }
}
