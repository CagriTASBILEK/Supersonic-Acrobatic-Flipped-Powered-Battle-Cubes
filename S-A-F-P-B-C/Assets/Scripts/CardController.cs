using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CardController : MonoBehaviour
{
    public static CardController instance { get; private set; }
    public CardSpawnArea spawnArea;
    [HideInInspector] public List<Vector3> randomSpawnPoints = new List<Vector3>();
    [SerializeField] private List<CardProperties> cardPropertiesList = new List<CardProperties>();
    public List<CardEntry> cardPrefabs = new List<CardEntry>();
    [SerializeField] public List<CardEntry> selectedCards = new List<CardEntry>();
    private Object[] cardDatas;
    private static readonly int Open = Animator.StringToHash("CardOpen");
    private static readonly int Close = Animator.StringToHash("CardClose");
    [HideInInspector] public int turnCount = 0;
    [HideInInspector] public int matchCount = 0;
    [HideInInspector] public int comboCount = 0;

    void Awake() => instance = this;

    private void Start()
    {
        CreateCards();
        PlayerPrefs.DeleteAll();
        UIManager.instance.pointCount.text = PlayerData.Point.ToString();
        UIManager.instance.comboCount.text = comboCount.ToString();
    }

    private void CreateCards()
    {
        cardDatas = Resources.LoadAll("CardDatas/", typeof(CardProperties));
        foreach (var cardsData in cardDatas)
        {
            cardPropertiesList.Add(cardsData as CardProperties);
            cardPropertiesList.Add(cardsData as CardProperties);
        }

        int totalCells = (int)(spawnArea.width * spawnArea.depth);
        while (cardPropertiesList.Count < totalCells)
        {
            int randomIndex = Random.Range(0, cardDatas.Length);
            cardPropertiesList.Add(cardDatas[randomIndex] as CardProperties);
            cardPropertiesList.Add(cardDatas[randomIndex] as CardProperties);
        }

        SpawnCards();
    }


    public IEnumerator SpawnCardsEnumerator()
    {
        yield return new WaitForSecondsRealtime(1);
        selectedCards.Clear();
        randomSpawnPoints.Clear();
        CreateCards();
    }

    private void SpawnCards()
    {
        transform.SetParent(transform);
        if (randomSpawnPoints.Count < 1)
        {
            foreach (var point in CardSpawnGridPoints())
            {
                randomSpawnPoints.Add(point);
            }
        }

        Shuffle(randomSpawnPoints);
        int i = 0;
        while (i < randomSpawnPoints.Count)
        {
            Vector3 spawnPosition = randomSpawnPoints[i];
            GameObject cardPrefab = Instantiate(cardPropertiesList[i].cardPrefab, spawnPosition,
                Quaternion.Euler(new Vector3(90, 0, 0)),
                transform);
            cardPrefab.name = cardPropertiesList[i].name;
            var cardEntry = cardPrefab.GetComponent<CardEntry>();
            cardEntry.cardEntry.frontSprite.sprite = cardPropertiesList[i].cardSprite;
            cardEntry.cardEntry.cardIndex = cardPropertiesList[i].cardIndex;
            cardEntry.cardEntry.cardPoint = cardPropertiesList[i].cardPoint;
            cardPrefabs.Add(cardEntry);
            i++;
        }

        if (spawnArea.width >= spawnArea.depth)
        {
            foreach (var cardControllerPrefab in cardPrefabs)
            {
                cardControllerPrefab.cardEntry.frontSprite.transform.localRotation =
                    Quaternion.Euler(new Vector3(0, 0, 90));
            }
        }
    }

    public void CardMatchControl(CardEntry selectedCard)
    {
        if (selectedCards.Contains(selectedCard))
        {
            return;
        }

        selectedCards.Add(selectedCard);
        var cardAnimator = selectedCard.gameObject.GetComponent<Animator>();

        cardAnimator.SetTrigger(Open);

        if (selectedCards.Count == 2)
        {
            turnCount++;
            UIManager.instance.turnCount.text = turnCount.ToString();
            StartCoroutine(CheckMatch());
        }
    }


    private IEnumerator CheckMatch()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (selectedCards[0].cardEntry.cardIndex == selectedCards[1].cardEntry.cardIndex)
        {
            matchCount++;
            comboCount++;
            PlayerData.Point += (selectedCards[0].cardEntry.cardPoint) * comboCount;
            UIManager.instance.pointCount.text = PlayerData.Point.ToString();
            UIManager.instance.comboCount.text = comboCount.ToString();
            UIManager.instance.matchCount.text = matchCount.ToString();
            Debug.Log("Match found!" + selectedCards[0].cardEntry.cardPoint + "       " + comboCount);

            foreach (var selectedCard in selectedCards)
            {
                cardPrefabs.Remove(selectedCard);
                Destroy(selectedCard.gameObject);
            }

            if (cardPrefabs.Count <= 0)
            {
                UIManager.instance.winPopup.SetActive(true);
            }
        }
        else
        {
            comboCount = 0;
            UIManager.instance.comboCount.text = comboCount.ToString();
            Debug.Log("No match found!");
            foreach (var selectedCard in selectedCards)
            {
                selectedCard.gameObject.GetComponent<Animator>().SetTrigger(Close);
            }
        }

        selectedCards.Clear();
    }


    IEnumerable<Vector3> CardSpawnGridPoints()
    {
        Vector3 spawnAreaPosition = transform.position + spawnArea.spawnPoint;
        if (spawnArea.width * spawnArea.depth % 2 != 0)
        {
            Debug.LogError("card size must be double!");
            yield break;
        }

        for (int x = 0; x < spawnArea.width; x++)
        {
            for (int z = 0; z < spawnArea.depth; z++)
            {
                yield return new Vector3(spawnAreaPosition.x + x, spawnAreaPosition.y, spawnAreaPosition.z + z);
            }
        }
    }

    void Shuffle<T>(List<T> inputList)
    {
        for (int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }

    [Serializable]
    public struct CardSpawnArea
    {
        [SerializeField] internal Vector3 spawnPoint;
        [SerializeField] internal float width;
        [SerializeField] internal float depth;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.red;
        foreach (var point in CardSpawnGridPoints())
        {
            Gizmos.DrawWireCube(point, new Vector3(1, 0, 1));
        }
#endif
    }
}