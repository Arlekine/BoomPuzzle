using UnityEngine;

namespace BoomPuzzle.Gameplay.LevelElements
{
    public class Trampoline : TypedCollisionHandler2D<Rigidbody2D>
    {
        [SerializeField] private float _force = 5f;

        protected override void OnEntered(Rigidbody2D target)
        {
            target.AddForce(transform.up * _force, ForceMode2D.Impulse);
        }
    }
}
