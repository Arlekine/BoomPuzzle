using System;
using UnityEngine;

namespace Model.ClickSystem
{
    public class ClickingSystem : IDisposable
    {
        private IClickInput _clickInput;
        private Camera _clickCamera;

        public ClickingSystem(IClickInput clickInput, Camera camera)
        {
            _clickInput = clickInput;
            _clickCamera = camera;

            _clickInput.Clicked += OnClicked;
        }
        
        public void Dispose()
        {
            _clickInput.Clicked -= OnClicked;
        }

        private void OnClicked(Vector2 screenPosition)
        {
            var clickRay = _clickCamera.ScreenPointToRay(screenPosition);
            RaycastHit2D hit = Physics2D.Raycast(clickRay.origin, clickRay.origin);

            if (hit.collider != null)
            {
                var clickableObject = hit.collider.gameObject.GetComponent<IClickable>();
                clickableObject?.Click();
            }
        }
    }
}