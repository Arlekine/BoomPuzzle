using System;
using UnityEngine;
using Zenject;

namespace Model.WinningLoseConditions
{
    public class FinalBlock : MonoBehaviour
    {
        [SerializeField] private CollisionStayHandler<FinalPlatform> _collisionStayHandler;

        private float _checkingTime;
        private CollisionStayHandler<FinalPlatform>.TimeTrigger _timeTrigger;
        
        public void Construct(float checkingTime)
        {
            _checkingTime = checkingTime;
            _timeTrigger = _collisionStayHandler.AddTimeTrigger(_checkingTime, OnTrigger);
        }

        public event Action StayingStart;
        public event Action StayingBreak;
        public event Action StayedOnFinalPlatform;

        public float CheckingTime => _checkingTime;

        private void TouchPlatform(FinalPlatform platform, Collision2D collision) => StayingStart?.Invoke(); 
        private void LeavePlatform(FinalPlatform platform, Collision2D collision) => StayingBreak?.Invoke(); 
        private void OnTrigger(FinalPlatform platform) => StayedOnFinalPlatform?.Invoke();

        private void OnEnable()
        {
            _collisionStayHandler.CollisionEnter += TouchPlatform;
            _collisionStayHandler.CollisionExit += LeavePlatform;
        }

        private void OnDisable()
        {
            _collisionStayHandler.CollisionEnter -= TouchPlatform;
            _collisionStayHandler.CollisionExit -= LeavePlatform;
        }

        private void OnDestroy()
        {
            if (_collisionStayHandler != null)
                _collisionStayHandler.RemoveTimeTrigger(_timeTrigger);
        }
    }
}