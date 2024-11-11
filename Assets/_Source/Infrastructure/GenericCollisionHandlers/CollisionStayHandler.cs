using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class CollisionStayHandler<T> : TypedCollisionHandler2D<T> 
{
    public class TimeTrigger
    {
        private Action<T> _action;

        public TimeTrigger(float timeToTrigger, Action<T> action)
        {
            TimeToTrigger = timeToTrigger;
            _action = action;
        }

        public float TimeToTrigger { get; }
        public void Trigger(T collidedObject) => _action(collidedObject);
    }

    private class CollisionHolder
    {
        private T _collidedObject;
        private float _collsionStartTime;
        
        private List<TimeTrigger> _triggeredTriggers = new List<TimeTrigger>();

        public CollisionHolder(T collidedObject, float collsionStartTime)
        {
            _collidedObject = collidedObject;
            _collsionStartTime = collsionStartTime;
        }

        public T CollidedObject => _collidedObject;

        public void TryTrigger(TimeTrigger timeTrigger)
        {
            if (_triggeredTriggers.Contains(timeTrigger))
                return;

            if (Time.time - _collsionStartTime > timeTrigger.TimeToTrigger)
            {
                _triggeredTriggers.Add(timeTrigger);
                timeTrigger.Trigger(_collidedObject);
            }
        }
    }

    private List<TimeTrigger> _timeTriggers = new List<TimeTrigger>();
    private List<CollisionHolder> _collisionHolders = new List<CollisionHolder>();

    public TimeTrigger AddTimeTrigger(float timeTrigger, Action<T> action)
    {
        var newTimeTrigger = new TimeTrigger(timeTrigger, action);
        _timeTriggers.Add(newTimeTrigger);
        return newTimeTrigger;
    }

    public bool RemoveTimeTrigger(TimeTrigger timeTrigger)
    {
        return _timeTriggers.Remove(timeTrigger);
    }

    protected override void OnEntered(T target)
    {
        var newCollisionHolder = new CollisionHolder(target, Time.time);
        _collisionHolders.Add(newCollisionHolder);
    }

    protected override void OnExit(T target)
    {
        _collisionHolders.RemoveAll(x => x.CollidedObject.Equals(target));
    }

    private void Update()
    {
        _collisionHolders.ForEach(x => _timeTriggers.ForEach(x.TryTrigger));
    }
}