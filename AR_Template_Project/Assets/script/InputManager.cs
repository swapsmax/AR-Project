using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class InputManager : ARBaseGestureInteractable
{
    //[SerializeField] private GameObject arObject;
    [SerializeField] private Camera arCam;
    //[SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject crosshair;

    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Touch touch;
    private Pose pose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //For manipulation of object
    protected override bool CanStartManipulationForGesture(TapGesture gesture){
        if(gesture.targetObject == null){
            return true;
        }
        return false;
    }

    protected override void OnEndManipulation(TapGesture gesture){
        if (gesture.isCanceled)
            return;
        if (gesture.targetObject != null || IsPointerOverUI(gesture))
            return;

        if (GestureTransformationUtility.Raycast(gesture.startPosition,_hits,xrOrigin)){
            GameObject placedObj = Instantiate(Datahandler.Instance.GetLight(), pose.position, pose.rotation);

            var anchorObj = new GameObject("PlacementAnchor");
            anchorObj.transform.position = pose.position;
            anchorObj.transform.rotation = pose.rotation;
            placedObj.transform.parent = anchorObj.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CrossHairCalculation();
    }

    bool IsPointerOverUI(TapGesture touch){
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData,results);
        return results.Count > 0;
    }

    void CrossHairCalculation(){
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        if (GestureTransformationUtility.Raycast(origin,_hits, xrOrigin)){
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90,0,0); // align to the ground
            //TODO: Align light to the ceiling or sides.

        }
    }
}
