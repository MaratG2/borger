using System;
using UnityEngine;

public class RecipeChecker : MonoBehaviour
{
    public CookingController Controller;
    public InputController InputController;
    public Flipper Flipper;
    public Recipe CurrentRecipe;

    public event Action LoseEvent;
    public event Action WinEvent;

    void Start()
    {
        Controller.CookEvent += OnCook;
        InputController.FlipEvent += OnFlip;
    }

    private void OnFlip()
    {
        if(!WinChecker())
            Flipper.Flip();
    }
    private bool WinChecker()
    {
        var roasts = Controller.Cooker.FryingBorger.Roast;
        foreach (var roast in roasts)
        {
            if (roast > CurrentRecipe.MaxFry || roast < CurrentRecipe.MinFry)
                return false;
        }

        WinEvent?.Invoke();
        return true;
    }

    private void OnCook(ESide side, float value)
    {
        if(value>CurrentRecipe.MaxFry)
            LoseEvent?.Invoke();
    }
}
