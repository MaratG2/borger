using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCounter : MonoBehaviour
{
    public Text text;
    public LevelController LevelController;
    private int _winCounts = 0;
    void Start()
    {
        LevelController.LevelWinEvent += OnWin;
    }

    private void OnWin()
    {
        _winCounts++;
        text.text = $"Пожарено: {_winCounts}";
    }
}
