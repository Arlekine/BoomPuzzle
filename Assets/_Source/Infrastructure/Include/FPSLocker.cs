using UnityEngine;

namespace Infrastructure
{
    public class FPSLocker : MonoBehaviour
    {
        [SerializeField] private int _targetFPS = 60;

        private void Start() => Application.targetFrameRate = _targetFPS;
    }
}
