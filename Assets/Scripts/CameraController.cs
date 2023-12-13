using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 30.0f;
    public float zoomFactor = 4.0f;
    public float targetZoom;
    public Vector3 startPosition;
    public float startZoom = 142.0f;

    private Camera cam;
    [SerializeField] PlayerInput PlayerInput;

    public InputActionAsset actions;
    private InputAction scrollAction;


    private void Awake()
    {
        scrollAction = actions.FindActionMap("Player").FindAction("Zoom");
        //scrollAction.started += OnScroll;
    }

    private void Start()
    {
        cam = Camera.main;
        startPosition = cam.transform.position;
        targetZoom = cam.orthographicSize;
    }


    // private void OnScroll(InputAction.CallbackContext started)
    private void Update()
    {
        // this is the "jump" action callback method
        // MouseScrollDirection = started.ReadValue<float>();

        float scrollData;
        scrollData = scrollAction.ReadValue<float>();

        // Debug.Log(scrollAction.started);

        Debug.Log(scrollData);

        targetZoom -= scrollData * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, 10f, startZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomFactor);

        if (cam.orthographicSize < targetZoom && targetZoom > startZoom - 40)
        {
            cam.transform.position = startPosition;
            targetZoom = startZoom;
        }
        else if (scrollData != 0)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            Vector3 zoomPoint = ray.GetPoint(cam.orthographicSize);
            zoomPoint.y = cam.transform.position.y;
            // cam.transform.position = zoomPoint;
        }
    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        // Disable the InputAction when the script is disabled
        // scrollAction.Disable();
        actions.FindActionMap("Player").Disable();
    }
}