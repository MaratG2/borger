using System;
using SpaceMarine.Control;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [NonSerialized]
    public float FlipInterruption = 1f;

    public bool IsBlocked = true;
    private bool _canFlip = true;

    public event Action FlipEvent;

    private void Start()
    {
        TouchController.TouchStartEvent += OnClick;
    }

    private void OnClick(TouchWrapper wrapper, bool isOverGameObjects)
    {
        var touch = wrapper.GetTouch();
        if(touch.phase!=TouchPhase.Began || IsBlocked || !_canFlip)
            return;
        _canFlip = false;
        Invoke(nameof(AllowFlipping), FlipInterruption);
        FlipEvent?.Invoke();
    }

    private void AllowFlipping() => _canFlip = true;
}
