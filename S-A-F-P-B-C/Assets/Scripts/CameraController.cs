using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance { get; private set; }
    void Awake() => instance = this;
    
    public Camera mainCamera;
    void Start()
    {
        CameraTransformControl();
    }

    public void CameraTransformControl()
    {
        mainCamera.transform.localPosition = CalculateGridCenter();
    }
    
    Vector3 CalculateGridCenter()
    {
        var cardController = CardController.instance;
       
        float gridWidth = cardController.spawnArea.width;
        float gridDepth = cardController.spawnArea.depth;
        float cameraHeight = gridWidth+gridDepth;

        float centerX = (gridWidth / 2f) - 0.5f;
        float centerZ = (gridDepth / 2f) - 0.5f;

        if (gridWidth >= gridDepth)
        {
            mainCamera.transform.localRotation = Quaternion.Euler(new Vector3(90,90,0));
        }
        else
        {
            mainCamera.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));
        }
        
        Vector3 centerOffset = new Vector3(centerX, cameraHeight, centerZ);
        
        Vector3 gridCenter = centerOffset;

        return gridCenter;
    }
}

