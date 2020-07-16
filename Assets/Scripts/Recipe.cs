using System;
using UnityEngine;

[Serializable]
public struct Recipe
{
    [Range(0,1)]
    public float MinFry;
    [Range(0,1)]
    public float MaxFry;
}
