using System;
using UnityEngine;

namespace Model.NonMomentalExplosion
{
    public interface IExplosionEventCaster
    {
        event Action<Vector2> Exploded;
    }
}