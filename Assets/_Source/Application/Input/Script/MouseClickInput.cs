using System;
using Model.ClickSystem;
using UnityEngine;
using Zenject;

namespace BoomPuzzle.Application
{
    public class MouseClickInput : IClickInput, ITickable
    {
        public event Action<Vector2> Clicked;

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
                Clicked?.Invoke(Input.mousePosition);
        }
    }
}
