using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardProperties", menuName = "GameCards/CardProperties", order = 1)]
public class CardProperties : ScriptableObject
{
    [SerializeField] public GameObject cardPrefab;
    [SerializeField] public String cardName;
    [SerializeField] public int cardIndex;
    [SerializeField] public Sprite cardSprite;
}