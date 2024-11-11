using System;
using UnityEngine;

namespace Model.ClickSystem
{
    public interface IClickInput
    {
        event Action<Vector2> Clicked;
    }
}