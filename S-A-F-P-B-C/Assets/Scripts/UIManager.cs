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
    public Button continueButton;
    public GameObject winPopup;
    
    void Awake() => instance = this;

    private void Start()
    {
        winPopup.SetActive(false);
        retryButton.onClick.AddListener(RetryGame);
        continueButton.onClick.AddListener(WinGame);
    }

    private void RetryGame()
    {
       GameManager.instance.RetryGame();
    }

    private void WinGame()
    {
        winPopup.SetActive(false);
        GameManager.instance.RetryGame();
    }
}
