using System;
using UnityEngine;

public class Cooker
{
    private Pan _pan;
    private ESide _currentSide;
    private Borger _borger;

    public Borger FryingBorger => _borger;

    public event Action<ESide, float> FryingEvent;

    public Cooker(Pan pan)
    {
        _pan = pan;
        _currentSide = ESide.BOTTOM;
        _borger = new Borger();
    }

    public void SwitchSide()
    {
        _currentSide = (ESide) (((int) _currentSide + 1) % 2);
    }

    public void Cook()
    {
        _borger.AddRoast(_currentSide, _pan.FrySpeed * Time.deltaTime);
        FryingEvent?.Invoke(_currentSide, _borger.GetRoast(_currentSide));
    }
}

public enum EGameState
{
    LOSE,
    WIN,
    CONTINUE
}