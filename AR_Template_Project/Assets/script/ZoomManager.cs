using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ZoomManager : MonoBehaviour
{
    private GraphicRaycaster raycaster;
    PointerEventData pData;
    EventSystem eventSystem;
    public Transform selectionPoint;

    //Singleton instance
    private static ZoomManager instance;
    public static ZoomManager Instance {
        get{
            if (instance == null){
                instance = FindObjectOfType<ZoomManager>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        pData = new PointerEventData(eventSystem);
        pData.position = selectionPoint.position;
    }

    public bool OnEntered(GameObject button){
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pData,results);

        foreach (RaycastResult result in results){
            if(result.gameObject == button){
                return true;
            }
        }
        return false;
    }
}
