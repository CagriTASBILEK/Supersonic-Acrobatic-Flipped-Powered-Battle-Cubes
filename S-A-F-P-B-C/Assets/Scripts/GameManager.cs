using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    void Awake() => instance = this;


    public void RetryGame()
    {
        var cardPrefabs = new List<CardEntry>(CardController.instance.cardPrefabs);

        foreach (var instanceCardPrefab in cardPrefabs)
        {
            CardController.instance.cardPrefabs.Remove(instanceCardPrefab);
            Destroy(instanceCardPrefab.gameObject);
        }
        CardController.instance.comboCount = 0;
        UIManager.instance.comboCount.text = CardController.instance.comboCount.ToString();
        
        StartCoroutine(CardController.instance.SpawnCardsEnumerator());
    }

}
