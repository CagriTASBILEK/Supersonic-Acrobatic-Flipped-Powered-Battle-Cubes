using System;
using UnityEngine;

public static class PlayerData
{
    public static int Point
    {
        get => PlayerPrefs.GetInt(nameof(Point), 0);
        set => PlayerPrefs.SetInt(nameof(Point), value);
    }
    
    public static int Width
    {
        get => PlayerPrefs.GetInt(nameof(Width), 2);
        set => PlayerPrefs.SetInt(nameof(Width), value);
    }
    public static int Depth
    {
        get => PlayerPrefs.GetInt(nameof(Depth), 2);
        set => PlayerPrefs.SetInt(nameof(Depth), value);
    }
}