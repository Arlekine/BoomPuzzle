using UnityEngine;

namespace Exploring.TimescaleWithPhysics
{
    public class TimeScaleSetter : MonoBehaviour
    {
        [Range(0.01f, 2f)][SerializeField] private float _timeScale = 1f;

        private void Update()
        {
            if (Time.timeScale != _timeScale)
                Time.timeScale = _timeScale;
        }
    }
}
