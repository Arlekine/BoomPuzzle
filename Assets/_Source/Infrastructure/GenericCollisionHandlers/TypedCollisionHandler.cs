using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class TypedCollisionHandler2D<T> : MonoBehaviour
{
    public event Action<T, Collision2D> CollisionEnter;
    public event Action<T, Collision2D> CollisionExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var targetComponent = collision.collider.GetComponent<T>();

        if (targetComponent != null)
        {
            CollisionEnter?.Invoke(targetComponent, collision);
            OnEntered(targetComponent);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var targetComponent = collision.collider.GetComponent<T>();

        if (targetComponent != null)
        {
            CollisionExit?.Invoke(targetComponent, collision);
            OnExit(targetComponent);
        }
    }

    protected virtual void OnEntered(T target){}
    protected virtual void OnExit(T target){}
}