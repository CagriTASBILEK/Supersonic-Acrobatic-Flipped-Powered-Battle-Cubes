using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEntry : MonoBehaviour
{
    public _CardEntry cardEntry;
    
    [Serializable]
    public class _CardEntry
    {
        [SerializeField] public SpriteRenderer frontSprite;
        [SerializeField] public int cardIndex;
        [SerializeField] public int cardPoint;
    }
}
