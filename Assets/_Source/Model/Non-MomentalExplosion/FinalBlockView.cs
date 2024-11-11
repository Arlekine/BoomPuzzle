using DG.Tweening;
using UnityEngine;

namespace Model.WinningLoseConditions
{
    public class FinalBlockView : MonoBehaviour
    {
        [SerializeField] private FinalBlock _model;
        [SerializeField] private Transform _animationTarget;

        private Sequence _currentAnimation;

        private void OnEnable()
        {
            _animationTarget.localScale = Vector3.zero;
            _model.StayingStart += StartAnimation;
            _model.StayingBreak += BreakAnimation;
        }

        private void OnDisable()
        {
            _model.StayingStart -= StartAnimation;
            _model.StayingBreak -= BreakAnimation;
        }

        private void StartAnimation()
        {
            BreakAnimation();
            _currentAnimation = DOTween.Sequence();
            _currentAnimation.Append(_animationTarget.DOScale(1f, _model.CheckingTime).SetEase(Ease.Linear));
            _currentAnimation.Append(_animationTarget.DOScale(1.1f, 0.2f));
            _currentAnimation.Append(_animationTarget.DOScale(1f, 0.1f));
        }

        private void BreakAnimation()
        {
            _currentAnimation?.Kill();
            _animationTarget.localScale = Vector3.zero;
        }
    }
}