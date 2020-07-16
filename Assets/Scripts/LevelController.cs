using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public CookingController Controller;
    public RecipeChecker RecipeChecker;
    public InputController Input;
    public RecipeBar[] Bars;
    [NonSerialized]
    public Recipe Recipe;
    [NonSerialized]
    public Pan pan;

    public event Action LevelStartEvent;
    public event Action LevelWinEvent;
    
    void Start()
    {
        RecipeChecker.CurrentRecipe = Recipe;
        RecipeChecker.WinEvent += OnWin;
        RecipeChecker.LoseEvent += OnLose;
    }

    public void StartLevel()
    {
        Controller.Initialize(new Cooker(pan));
        RecipeChecker.CurrentRecipe = Recipe;
        Input.IsBlocked = false;

        for(int i =0; i<Bars.Length; i++)
            Bars[i].Initialization(Recipe);
        LevelStartEvent?.Invoke();
    }

    private void OnWin()
    {
        LevelWinEvent?.Invoke();
    }

    private void OnLose()
    {
        Debug.Log("Loooooser!");
    }
}
