using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CardController : MonoBehaviour
{
    public CardSpawnArea spawnArea;
    [HideInInspector]public List<Vector3> randomSpawnPoints = new List<Vector3>();
    [SerializeField] private List<CardProperties> cardPropertiesList = new List<CardProperties>();
    [SerializeField] private List<GameObject> cardPrefabs = new List<GameObject>();
    private Object[] cardDatas;
    private void Start()
    {
        CreateCards();
    }

    private void CreateCards()
    {
        cardDatas = Resources.LoadAll("CardDatas/" , typeof(CardProperties));
        foreach (var cardsData in cardDatas)
        {
            cardPropertiesList.Add(cardsData as CardProperties);
        }
        SpawnCards();
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
           GameObject cardPrefab = Instantiate(cardPropertiesList[i].cardPrefab, spawnPosition, Quaternion.Euler(new Vector3(90,0,0)),
                transform);
           cardPrefab.name = cardPropertiesList[i].name;
           cardPrefabs.Add(cardPrefab);
            i++;
        }
    }
    
    IEnumerable<Vector3> CardSpawnGridPoints()
    {
        Vector3 spawnAreaPosition = transform.position + spawnArea.spawnPoint;
        for (int x = 0; x <spawnArea.width; x++)
        {
            for (int z = 0; z < spawnArea.depth; z++)
            {
                yield return new Vector3(spawnAreaPosition.x+x, spawnAreaPosition.y, spawnAreaPosition.z+z);
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