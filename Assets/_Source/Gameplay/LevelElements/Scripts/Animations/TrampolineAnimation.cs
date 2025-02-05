using DG.Tweening;
using UnityEngine;

namespace BoomPuzzle.Gameplay.LevelElements
{
    public class TrampolineAnimation : MonoBehaviour
    {
        [SerializeField] private Trampoline _trampoline;

        [Space]
        [SerializeField] private Transform _arrow;
        [SerializeField] private AnimationCurve _yCurve;
        [SerializeField] private float _maxValue;
        [SerializeField] private float _animationTime;

        private Sequence _sequence;

        private void OnEnable()
        {
            _trampoline.CollisionEnter += Play;
        }

        private void OnDisable()
        {
            _trampoline.CollisionEnter -= Play;
            _arrow?.DOKill();
        }

        private void Play(Rigidbody2D rigidbody, Collision2D collision) => _arrow.DOLocalMoveY(_maxValue, _animationTime).SetEase(_yCurve);
    }
}
