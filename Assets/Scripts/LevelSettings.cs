using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public LevelController Controller;
    [Range(0, .5f)]
    public float StartThreshold = .25f;

    public float DifficultModif = .9f;
    public int WinCount = 0;
    void Start()
    {
        Controller.LevelWinEvent += OnWin;
        Initialization();
    }

    private void OnWin()
    {
        WinCount++;
        Initialization();
    }

    private void Initialization()
    {
        var currentThreshold = StartThreshold * Mathf.Pow(DifficultModif, WinCount);
        var shift = Random.value * (.5f - currentThreshold);

        Controller.Recipe = new Recipe()
        {
            MinFry = .5f + shift,
            MaxFry = .5f + shift + currentThreshold
        };
        Controller.pan = new Pan()
        {
            FrySpeed = Random.value * .2f + .3f
        };
    }
}
