using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    public TextMeshProUGUI pointCount;
    public TextMeshProUGUI comboCount;
    
    void Awake() => instance = this;
}
