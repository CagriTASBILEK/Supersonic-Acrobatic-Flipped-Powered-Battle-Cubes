using System;
using UnityEngine;

public static class PlayerData
{
    public static int Point
    {
        get => PlayerPrefs.GetInt(nameof(Point), 0);
        set => PlayerPrefs.SetInt(nameof(Point), value);
    }
    
}