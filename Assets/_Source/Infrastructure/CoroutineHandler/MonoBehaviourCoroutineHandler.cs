using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public class MonoBehaviourCoroutineHandler : ICoroutineHandler
    {
        private MonoBehaviour _monoBehaviour;

        public MonoBehaviourCoroutineHandler(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
        }

        public Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return _monoBehaviour.StartCoroutine(coroutine);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            _monoBehaviour.StopCoroutine(coroutine);
        }
    }
}