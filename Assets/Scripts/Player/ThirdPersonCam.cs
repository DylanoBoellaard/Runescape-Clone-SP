using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//using System.Drawing;
//using System.Windows.Forms;

public class ThirdPersonCam : MonoBehaviour
{
    // https://www.youtube.com/watch?v=UCwwn2q4Vys

    [Header("References Camera")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public CameraStyle currentstyle;
    [Header("References Camera Zoom")]
    public float cameraSpeed = 10f;
    public float topMinRadius = 0.5f;
    public float topMaxRadius = 5f;
    public float middleMinRadius = 2f;
    public float middleMaxRadius = 10f;
    public float bottomMinRadius = 5f;
    public float bottomMaxRadius = 15f;

    //private Vector3 previousCursorPosition; // Variable for previous mouse cursor position

    // Uncomment code for setting mouse position

    public enum CameraStyle
    {
        Basic,
        Topdown
    }

    // Start is called before the first frame update
    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom; // Used for restricting cam movement to middle mouse btn
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // Rotate player object
        if (currentstyle == CameraStyle.Basic || currentstyle == CameraStyle.Topdown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        // Zoom in and out with the Cinemachine camera using scrollwheel by modifying orbit distance (not FOV)
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        CinemachineFreeLook freeLook = GetComponent<CinemachineFreeLook>();

        // Adjust the camera distance based on the scroll wheel input
        float newRadius = freeLook.m_Orbits[0].m_Radius - scrollWheelInput * cameraSpeed;
        freeLook.m_Orbits[0].m_Radius = Mathf.Clamp(newRadius, topMinRadius, topMaxRadius);

        newRadius = freeLook.m_Orbits[1].m_Radius - scrollWheelInput * cameraSpeed;
        freeLook.m_Orbits[1].m_Radius = Mathf.Clamp(newRadius, middleMinRadius, middleMaxRadius);

        newRadius = freeLook.m_Orbits[2].m_Radius - scrollWheelInput * cameraSpeed;
        freeLook.m_Orbits[2].m_Radius = Mathf.Clamp(newRadius, bottomMinRadius, bottomMaxRadius);
    }

    // Restricts the camera to move only when the user holds the middle mousebutton (2)
    // TO DO: Set cursor position to before it locks and becomes invisible  ( https://answers.unity.com/questions/330661/setting-the-mouse-position-to-specific-coordinates.html - answer by zachwuzhere)
    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X") // Horizontal movement
        {
            if (Input.GetMouseButton(2))
            {
                //previousCursorPosition = Input.mousePosition;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Debug.Log("Camera Mouse X should be moving");

                return UnityEngine.Input.GetAxis("Mouse X");
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //SetCursorPosition(previousCursorPosition);

                return 0;
            }
        }
        else if (axisName == "Mouse Y") // Vertical movement
        {
            if (Input.GetMouseButton(2))
            {
                //previousCursorPosition = Input.mousePosition;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Debug.Log("Camera Mouse Y should be moving");

                return UnityEngine.Input.GetAxis("Mouse Y");
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //SetCursorPosition(previousCursorPosition);

                return 0;
            }
        }
        return UnityEngine.Input.GetAxis(axisName);
    }

    /*private void SetCursorPosition(Vector3 position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        UnityEngine.Input.mousePosition = screenPosition;
    }*/

}
