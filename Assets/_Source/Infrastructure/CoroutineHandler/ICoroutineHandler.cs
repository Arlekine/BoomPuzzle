using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public interface ICoroutineHandler
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine copoutine);
    }
}
