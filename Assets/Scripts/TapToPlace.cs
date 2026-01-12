using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class TapToPlace : MonoBehaviour
{
    public GameObject placementObject;
    ARRaycastManager raycastManager;
    static List<ARRaycastHit> hits = new();

    void Start()
    {
        raycastManager = FindFirstObjectByType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            placementObject.SetActive(true);
            placementObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
        }
    }
}
