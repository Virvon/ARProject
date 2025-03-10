using System;
using UnityEngine;

namespace Assets.Sources.Services.InputService
{
    public class InputService : IInputService
    {
        public event Action<Vector2> Clicked;
        public event Action<Vector2> Dragged;
        public event Action DragEnded;
        public event Action<float> Zoomed;

        private bool _isDragged;
        private float _lastDistance;
        private bool _isActive;

        public InputService()
        {
            _isDragged = false;

            SetActive(true);
        }

        public void Tick()
        {
            if (Input.touchCount <= 0 || _isActive == false)
                return;
            else if (Input.touchCount == 1)
                FirstTouchHandle();
            else
                SecondTouchHandle();
        }

        public void SetActive(bool isActive) =>
            _isActive = isActive;

        private void SecondTouchHandle()
        {
            if (_isDragged)
                DragEnded?.Invoke();

            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            if (secondTouch.phase == TouchPhase.Began)
                _lastDistance = (firstTouch.position - secondTouch.position).magnitude;

            if(firstTouch.phase == TouchPhase.Moved || secondTouch.phase == TouchPhase.Moved)
            {
                float magnitude = (firstTouch.position - secondTouch.position).magnitude;
                float delta = magnitude - _lastDistance;
                _lastDistance = magnitude;

                Zoomed?.Invoke(delta);
            }
        }

        private void FirstTouchHandle()
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Clicked?.Invoke(touch.position);
                    _isDragged = true;
                    break;
                case TouchPhase.Moved:
                    if (_isDragged)
                        Dragged?.Invoke(touch.position);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (_isDragged)
                    {
                        _isDragged = false;
                        DragEnded?.Invoke();
                    }
                    break;
            }
        }
    }
}
