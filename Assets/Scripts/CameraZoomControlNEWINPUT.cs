using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class CameraZoomControlNEWINPUT : MonoBehaviour
{
    // Camera zoom stuff
    //[SerializeField] private InputActionAsset inputProvider;            // Used to grab the new input manager stuff
    [SerializeField] private CinemachineFreeLook freeLookCameraToZoom;  // Used to get the freelook camera component
    [SerializeField] private float zoomSpeed = 1f;                      // The zoom speed
    [SerializeField] private float zoomAcceleration = 2.5f;             // For smooth zoomin in or out
    [SerializeField] private float zoomInnerRange = 3;                  // Max to zoom in
    [SerializeField] private float zoomOuterRange = 50;                 // Max to zoom out

    private float currentMiddleRigRadius = 5f;  // Is the same as Topdown Cam > orbits > MiddleRig
    private float newMiddleRigRadius = 5f;      // ^^ Same as above ^^

    [SerializeField] private float zoomYAxis = 0f; // Captures the scrollwheel value


    // Camera zoom functions
    public float ZoomYAxis
    {
        get { return zoomYAxis; }
        set
        {
            if (zoomYAxis == value) return;
            zoomYAxis = value;
            AdjustCameraZoomIndex(ZoomYAxis);
        }
    }

    private void Awake()
    {
        // Finds the "Free Look Camera Controls" action map > finds action "Mouse Zoom" > reads the value of the scroll wheel and sets it to ZoomYAxis (scrollwheel up = 120f, scroll wheel down = -120f)
            //inputProvider.FindActionMap("Free Look Camera Controls").FindAction("Mouse Zoom").performed += cntxt => ZoomYAxis = cntxt.ReadValue<float>();
        // When user stops scrolling, ZoomYAxis becomes 0f
            //inputProvider.FindActionMap("Free Look Camera Controls").FindAction("Mouse Zoom").canceled += cntxt => ZoomYAxis = 0f;
    }

    private void OnEnable()
    {
        //inputProvider.FindAction("Mouse Zoom").Enable();
    }

    private void OnDisable()
    {
        //inputProvider.FindAction("Mouse Zoom").Disable();
    }

    private void LateUpdate()
    {
        UpdateZoomLevel();
    }

    private void UpdateZoomLevel()
    {
        if (currentMiddleRigRadius == newMiddleRigRadius) { return; } // If nothing has happened, exit out of it (return)

        // If something has happened, does this \/
        // Lerp between current- and newMiddleRigRadius by the zoomAcceleration * Time.deltaTime
        currentMiddleRigRadius = Mathf.Lerp(currentMiddleRigRadius, newMiddleRigRadius, zoomAcceleration * Time.deltaTime);

        // For the maximum and minimum zoom
        currentMiddleRigRadius = Mathf.Clamp(currentMiddleRigRadius, zoomInnerRange, zoomOuterRange);

        // Selects camera and changes the Radius of the top rig orbit to currentMiddleRigradius
        freeLookCameraToZoom.m_Orbits[1].m_Radius = currentMiddleRigRadius;

        // Sets height of middle rig orbit [0] and bottom rig orbit [2] to the orbit radius of [1]
        freeLookCameraToZoom.m_Orbits[0].m_Height = freeLookCameraToZoom.m_Orbits[1].m_Radius;
        freeLookCameraToZoom.m_Orbits[2].m_Height = -freeLookCameraToZoom.m_Orbits[1].m_Radius;
    }

    public void AdjustCameraZoomIndex(float zoomYAxis)
    {
        if (zoomYAxis == 0f) { return; }

        if (zoomYAxis < 0)
        {
            newMiddleRigRadius = currentMiddleRigRadius + zoomSpeed;
        }

        if (zoomYAxis > 0)
        {
            newMiddleRigRadius = currentMiddleRigRadius - zoomSpeed;
        }
    }
}
