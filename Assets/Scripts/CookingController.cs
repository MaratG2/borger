using System;
using UnityEngine;

public class CookingController : MonoBehaviour
{
    public Flipper Flipper;
    public Cooker Cooker;
    private bool _isLanded;
    private bool _isFrying;

    public event Action<ESide, float> CookEvent;

    void Start()
    {
        Flipper.TakeOffEvent += () => _isLanded = false;
        Flipper.PloppedEvent += () =>
        {
            _isLanded = true;
            Cooker.SwitchSide();
        };
    }

    public void Initialize(Cooker cooker)
    {
        Cooker = cooker;
        Cooker.FryingEvent += OnCook;
        _isFrying = true;
        _isLanded = false;
    }

    private void OnCook(ESide side, float value) => 
        CookEvent?.Invoke(side, value);

    private void Update()
    {
        if (_isLanded&&_isFrying)
            Cooker.Cook();
    }
}
