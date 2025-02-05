using UnityEngine;

public class LowGravityZone : TypedTrigger<Rigidbody2D>
{
    [SerializeField] private float _lowGravity = 0.3f;

    protected override void OnEnterTriggered(Rigidbody2D other)
    {
        other.gravityScale = _lowGravity;
    }

    protected override void OnExitTriggered(Rigidbody2D other)
    {
        other.gravityScale = 1f;
    }
}
