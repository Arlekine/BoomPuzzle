using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoomPuzzle.Gameplay.LevelElements
{
    public class MovingPlatform : MonoBehaviour
    {
        private enum MovingAlignment
        {
            Horizontal,
            Vertical
        }

        private const float GIZMOS_BORDERS_OFFSET = 0.25f;

        private static Dictionary<MovingAlignment, Vector3> GizmosBordersDirections = new Dictionary<MovingAlignment, Vector3>()
        {
            { MovingAlignment.Horizontal, Vector2.up},
            { MovingAlignment.Vertical, Vector2.right}
        };

        private static Dictionary<MovingAlignment, Vector3> Directions = new Dictionary<MovingAlignment, Vector3>()
        {
            { MovingAlignment.Horizontal, Vector2.right},
            { MovingAlignment.Vertical, Vector2.up}
        };

        [Tooltip("Shouldn't be the same object as the one with thus script on")]
        [SerializeField] private Transform _movablePart;
        [SerializeField] private float _movingRange = 2.6f;
        [SerializeField] private MovingAlignment _movingAlignment;
        
        [Space]
        [SerializeField] private float _movingSpeed = 1.3f;
        [SerializeField] private bool _isInitialDirectionForward = true;
        [SerializeField] private bool _isActivatedOnStart = false;
        [SerializeField] private bool _isLoop = false;

        private bool _isActivated;
        private bool _isCurrentDirectionForward;

        private Vector3 CurrentDestination => _isCurrentDirectionForward ? EndPosition : StartPosition;

        public Vector3 StartPosition => transform.position + Directions[_movingAlignment] * _movingRange * 0.5f;
        public Vector3 EndPosition => transform.position - Directions[_movingAlignment] * _movingRange * 0.5f;
        public Transform MovablePart => _movablePart;

        [EditorButton]
        public void Activate()
        {
            if (_isActivated && _isLoop)
                return;

            if (_isActivated)
                _isCurrentDirectionForward = !_isCurrentDirectionForward;
            
            _isActivated = true;
        }


        [EditorButton]
        public void Deactivate() => _isActivated = false;

        private void Awake()
        {
            _isCurrentDirectionForward = _isInitialDirectionForward;

            if (_isActivatedOnStart)
                Activate();
        }

        private void FixedUpdate()
        {
            if (_isActivated)
            {
                _movablePart.position = Vector3.MoveTowards(_movablePart.position, CurrentDestination, _movingSpeed * Time.fixedDeltaTime);

                if (Vector3.Distance(_movablePart.position, CurrentDestination) <= float.Epsilon)
                {
                    _isCurrentDirectionForward = !_isCurrentDirectionForward;

                    if (_isLoop == false)
                        _isActivated = false;
                }

            }
        }

        private void OnDrawGizmos()
        {
            if (_movablePart == null)
                return;

            var blocks = _movablePart.GetComponentsInChildren<BoxCollider2D>();

            if (blocks.Length > 0)
            {
                var blockSize = blocks[0].size.x;
                var blocksPositionBorders = new Vector3();

                blocksPositionBorders.x = _movingAlignment == MovingAlignment.Horizontal ? blocks.Max(x => x.transform.localPosition.x) : 0f;
                blocksPositionBorders.y = _movingAlignment == MovingAlignment.Vertical ? blocks.Max(x => x.transform.localPosition.y) : 0f;

                var sizeCountedStartPosition = StartPosition + (blocksPositionBorders + Directions[_movingAlignment] * blockSize * 0.5f);
                var sizeCountedEndPosition = EndPosition - (blocksPositionBorders + Directions[_movingAlignment] * blockSize * 0.5f);

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(StartPosition, EndPosition);
                Gizmos.DrawLine(StartPosition + GizmosBordersDirections[_movingAlignment] * GIZMOS_BORDERS_OFFSET, StartPosition - GizmosBordersDirections[_movingAlignment] * GIZMOS_BORDERS_OFFSET);
                Gizmos.DrawLine(EndPosition + GizmosBordersDirections[_movingAlignment] * GIZMOS_BORDERS_OFFSET, EndPosition - GizmosBordersDirections[_movingAlignment] * GIZMOS_BORDERS_OFFSET);


                Gizmos.color = Color.red;
                Gizmos.DrawLine(sizeCountedStartPosition, sizeCountedEndPosition);
                Gizmos.DrawLine(sizeCountedStartPosition + GizmosBordersDirections[_movingAlignment] * GIZMOS_BORDERS_OFFSET, sizeCountedStartPosition - GizmosBordersDirections[_movingAlignment] * GIZMOS_BORDERS_OFFSET);
                Gizmos.DrawLine(sizeCountedEndPosition + GizmosBordersDirections[_movingAlignment] * GIZMOS_BORDERS_OFFSET, sizeCountedEndPosition - GizmosBordersDirections[_movingAlignment] * GIZMOS_BORDERS_OFFSET);
            }
        }
    }
}