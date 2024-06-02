using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    public TextMeshProUGUI pointCount;
    public TextMeshProUGUI comboCount;
    public TextMeshProUGUI turnCount;
    public TextMeshProUGUI matchCount;
    public Button retryButton;
    
    void Awake() => instance = this;

    private void Start()
    {
        retryButton.onClick.AddListener(RetryGame);
    }

    private void RetryGame()
    {
       GameManager.instance.RetryGame();
    }
}
