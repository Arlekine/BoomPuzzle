using System;
using Infrastructure;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace BoomPuzzle.Gameplay.LevelElements
{
    [RequireComponent(typeof(MovingPlatform))]
    public class MovingPlatformHelper : MonoBehaviour
    {
        [SerializeField] private MovingPlatform _movingPlatform;
        [SerializeField] private Rigidbody2D _blockPrefab;

        private float BlockSize => _blockPrefab.GetComponent<BoxCollider2D>().size.x;
        private Vector3 HorizontalAlignment => Vector3.right;
        private Vector3 VerticalAlignment => Vector3.up;

        [EditorButton]
        private void CreateHorizontalBlocks(int blocksCount) => CreateBlocks(blocksCount, HorizontalAlignment);

        [EditorButton]
        private void CreateVerticalBlocks(int blocksCount) => CreateBlocks(blocksCount, VerticalAlignment);
        
        [EditorButton]
        private void SetStartPosition() => _movingPlatform.MovablePart.position = _movingPlatform.StartPosition;

        [EditorButton]
        private void SetCenterPosition() => _movingPlatform.MovablePart.position = (_movingPlatform.StartPosition + _movingPlatform.EndPosition) * 0.5f;

        [EditorButton]
        private void SetEndPosition() => _movingPlatform.MovablePart.position = _movingPlatform.EndPosition;

        private void CreateBlocks(int blocksCount, Vector3 alignment)
        {
            if (_movingPlatform.MovablePart == null || _blockPrefab == null)
                throw new Exception($"Set {nameof(_movingPlatform.MovablePart)} and {nameof(_blockPrefab)} first");

            if (_movingPlatform.MovablePart.childCount > 0)
                _movingPlatform.MovablePart.DestroyAllChildrenImmediately();

#if UNITY_EDITOR
            var startSpawnPosition = -alignment * GetBlocksCountOffset(blocksCount);

            for (int i = 0; i < blocksCount; i++)
            {
                var newBlock = PrefabUtility.InstantiatePrefab(_blockPrefab, _movingPlatform.MovablePart) as Rigidbody2D;
                newBlock.transform.localPosition = startSpawnPosition + alignment * BlockSize * i;
            }
#endif
        }

        private float GetBlocksCountOffset(int blockCount)
        {
            if (blockCount % 2 == 0)
                return (blockCount * 0.5f - 1) * BlockSize + BlockSize * 0.5f;
            else
                return (blockCount - 1) * 0.5f * BlockSize;
        }

        private void OnValidate()
        {
            _movingPlatform = GetComponent<MovingPlatform>();
        }

        private void OnDrawGizmos()
        {
            _movingPlatform.MovablePart.position = MathExtensions.GetClosestPointOnLine(_movingPlatform.StartPosition,
                _movingPlatform.EndPosition - _movingPlatform.StartPosition, _movingPlatform.MovablePart.position);
        }
    }
}