using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    void Awake() => instance = this;


    public void WinGame()
    {
        var cardController = CardController.instance;
        var uiManager = UIManager.instance;
        var cardPrefabs = new List<CardEntry>(cardController.cardPrefabs);

        foreach (var instanceCardPrefab in cardPrefabs)
        {
            cardController.cardPrefabs.Remove(instanceCardPrefab);
            Destroy(instanceCardPrefab.gameObject);
        }

        cardController.matchCount = 0;
        cardController.turnCount = 0;
        cardController.comboCount = 0;
        uiManager.comboCount.text = cardController.comboCount.ToString();
        uiManager.matchCount.text = cardController.matchCount.ToString();
        uiManager.turnCount.text = cardController.turnCount.ToString();


        WinGameLevelControl();
        StartCoroutine(cardController.SpawnCardsEnumerator());
    }

    private void WinGameLevelControl()
    {
        var cardController = CardController.instance;
        
        if (cardController.spawnArea.depth % 2 == 0 && cardController.spawnArea.width % 2 == 0)
        {
            cardController.spawnArea.depth++;
        }
        else if (cardController.spawnArea.depth % 2 == 0 || cardController.spawnArea.width % 2 == 0)
        {
            cardController.spawnArea.width++;
        }
        
        if ((cardController.spawnArea.width * cardController.spawnArea.depth) % 2 != 0)
        {
            cardController.spawnArea.depth++; 
        }
        
        CameraController.instance.CameraTransformControl();
    }

    public void RetryGame()
    {
        var cardController = CardController.instance;
        var uiManager = UIManager.instance;
        var cardPrefabs = new List<CardEntry>(cardController.cardPrefabs);

        foreach (var instanceCardPrefab in cardPrefabs)
        {
            cardController.cardPrefabs.Remove(instanceCardPrefab);
            Destroy(instanceCardPrefab.gameObject);
        }

        cardController.matchCount = 0;
        cardController.turnCount = 0;
        cardController.comboCount = 0;
        uiManager.comboCount.text = cardController.comboCount.ToString();
        uiManager.matchCount.text = cardController.matchCount.ToString();
        uiManager.turnCount.text = cardController.turnCount.ToString();

        StartCoroutine(cardController.SpawnCardsEnumerator());
    }
}