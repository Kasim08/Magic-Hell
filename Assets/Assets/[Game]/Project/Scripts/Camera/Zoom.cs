using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/*
public class Zoom : MonoBehaviour
{
    public CinemachineVirtualCamera vcamera;
    CinemachineComponentBase cmbase;
    public float defaultFOV = 40;
    public float maxZoomFOV = 30;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 10;
    bool zoom = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!zoom)
            {
                zoom = true;
                currentZoom += sensitivity * .05f;
                currentZoom = Mathf.Clamp01(currentZoom);
                vcamera.m_Lens.FieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
            }
            else { 
                zoom = false;
                currentZoom += -sensitivity * .05f;
                currentZoom = Mathf.Clamp01(currentZoom);
                vcamera.m_Lens.FieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
            }
        }
        //// Update the currentZoom and the camera's fieldOfView. MidButton use.
        //currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        //currentZoom = Mathf.Clamp01(currentZoom);
        //vcamera.m_Lens.FieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}

*/


public class Zoom : MonoBehaviour
{
    public CinemachineVirtualCamera vcamera;
    CinemachineComponentBase cmbase;
    public float defaultFOV = 40;
    public float maxZoomFOV = 30;
    public float minZoomFOV = 10;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 10;
    bool zoom = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!zoom)
            {
                zoom = true;
                currentZoom += sensitivity * .05f;
                currentZoom = Mathf.Clamp01(currentZoom);
                vcamera.m_Lens.FieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);        
            }
            else
            {
                zoom = false;
                currentZoom += -sensitivity * .05f;
                currentZoom = Mathf.Clamp01(currentZoom);
                vcamera.m_Lens.FieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
            }
        }
        if (zoom)
        {
            // Update the currentZoom and the camera's fieldOfView. MidButton use.
            currentZoom += Input.GetAxis("Mouse ScrollWheel") * sensitivity * .05f;
            currentZoom = Mathf.Clamp01(currentZoom);
            vcamera.m_Lens.FieldOfView = Mathf.Lerp(minZoomFOV, maxZoomFOV, currentZoom);
        }
    }
}