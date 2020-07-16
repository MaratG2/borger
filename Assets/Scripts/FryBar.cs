using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FryBar : MonoBehaviour
{
    public ESide Side;
    public CookingController CookingController;
    public LevelController LevelController;
    public Image Bar;

    public void Initialization()
    {
        Bar.fillAmount = 0;
    }
    private void Start()
    {
        CookingController.CookEvent += UpdateBar;
        LevelController.LevelStartEvent += Initialization;
    }

    private void UpdateBar(ESide currentSide, float value)
    {
        if (Side == currentSide)
            Bar.fillAmount = value;
    }
}
