using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{  
    public Camera mainCamera;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                CardEntry card = hit.collider.GetComponent<CardEntry>();
                if (card != null)
                {
                    Debug.Log("Card Name: " + card.cardEntry.cardIndex);
                }
                else
                {
                    Debug.Log("Clicked object is not a card.");
                }
            }
            else
            {
                Debug.Log("Clicked on empty space.");
            }
        }
    }
}
