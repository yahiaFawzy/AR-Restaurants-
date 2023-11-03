using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Lean.Touch;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    public GameObject m_PlacedPrefab { get; set; }
    public ARSession arSession;

    [Header("UI Stage")]
    public GameObject scanningStage;
    public GameObject placementIndicatorStage;


    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Vector2 centerOfScreen;
    private bool isScanningStage = true;
    private bool isPlaced = false;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Start()
    {
        centerOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        if (isPlaced) return;

        if (raycastManager.Raycast(centerOfScreen, hits, TrackableType.PlaneWithinPolygon))
        {
            if (isScanningStage)
            {
                isScanningStage = false;

                scanningStage.SetActive(false);
                placementIndicatorStage.SetActive(true);

                SpawnModel(hits[0].pose);
            }
            else
            {
                spawnedObject.transform.SetPositionAndRotation(hits[0].pose.position, hits[0].pose.rotation);
            }
        }

        if (Input.touchCount > 0 && spawnedObject != null)
        {
            isPlaced = true;
            placementIndicatorStage.SetActive(false);
        }
    }

    private void SpawnModel(Pose hitPose)
    {
        spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
        spawnedObject.AddComponent<LeanPinchScale>();
        spawnedObject.AddComponent<ARModelViewController>();
    }

    public void Reset3DView()
    {
        Destroy(spawnedObject);

        isPlaced = false;
        isScanningStage = true;

        scanningStage.SetActive(true);
        placementIndicatorStage.SetActive(false);

        arSession.Reset();
    }
}