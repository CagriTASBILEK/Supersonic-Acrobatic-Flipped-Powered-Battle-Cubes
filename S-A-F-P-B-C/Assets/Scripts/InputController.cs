using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{  
    public Camera mainCamera;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CardController.instance.selectedCards.Count != 2)
        {
            if (EventSystem.current.currentSelectedGameObject)
                if (EventSystem.current.currentSelectedGameObject.layer == 5)
                    return;
            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                CardEntry card = hit.collider.GetComponent<CardEntry>();
                if (card != null)
                {
                    Debug.Log("Card Name: " + card.cardEntry.cardIndex);
                    CardController.instance.CardMatchControl(card);
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
