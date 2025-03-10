using Assets.Sources.Services.TickService;
using System;
using UnityEngine;

namespace Assets.Sources.Services.InputService
{
    public interface IInputService : ITickable
    {
        event Action<Vector2> Clicked;
        event Action<Vector2> Dragged;
        event Action DragEnded;
        event Action<float> Zoomed;
    }
}
