using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBar : MonoBehaviour
{
    public Image[] images;

    public void Initialization(Recipe recipe)
    {
        images[0].fillAmount = recipe.MinFry;
        images[1].fillAmount = 1 - recipe.MaxFry;
    }
}
