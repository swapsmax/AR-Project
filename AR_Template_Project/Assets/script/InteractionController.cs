using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

[Serializable]
public class ARObjectPlacedEvent : UnityEvent<InteractionController, GameObject> { }
public class InteractionController : MonoBehaviour
{
    
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private Camera arCam;

    [SerializeField]
        [Tooltip("A GameObject to place when a raycast from a user touch hits a plane.")]
        GameObject m_PlacementPrefab;

        /// <summary>
        /// A <see cref="GameObject"/> to place when a raycast from a user touch hits a plane.
        /// </summary>
        public GameObject placementPrefab
        {
            get => m_PlacementPrefab;
            set => m_PlacementPrefab = value;
        }

        [SerializeField] private GameObject crosshair;
        [SerializeField, Tooltip("Called when the this interactable places a new GameObject in the world.")]
        ARObjectPlacedEvent m_OnObjectPlaced = new ARObjectPlacedEvent();

        /// <summary>
        /// The event that is called when the this interactable places a new <see cref="GameObject"/> in the world.
        /// </summary>
        public ARObjectPlacedEvent onObjectPlaced
        {
            get => m_OnObjectPlaced;
            set => m_OnObjectPlaced = value;
        }

        static readonly List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
        static GameObject s_TrackablesObject;

        private Pose pose;

        bool IsPointerOverUI(Touch touch)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = new Vector2(touch.position.x, touch.position.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }

        private void Update()
        {
            if(crosshair.activeSelf)   
                CrosshairCalculation();
        }

        void CrosshairCalculation()
        {
            Vector3 origin = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
            Ray ray = arCam.ScreenPointToRay(origin);

            if (_raycastManager.Raycast(ray, s_Hits))
            {
                pose = s_Hits[0].pose;
                crosshair.transform.position = pose.position;
                crosshair.transform.eulerAngles = new Vector3(90,0,0);
            }
        }

        public void onFinishPlacement()
        {
            crosshair.SetActive(false);
        }
        
    }