using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform myPlayerHead;

    private float startFOV, tarquetFOV;
    public float FOVSpeed;
    private Camera myCamera;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myCamera = GetComponent<Camera>(); //GetComponent works to components that is inside the main components

        startFOV = myCamera.fieldOfView;
        tarquetFOV = startFOV;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = myPlayerHead.position;
        transform.rotation = myPlayerHead.rotation;

        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, tarquetFOV, FOVSpeed * Time.deltaTime);
    }

    public void ZoomIn(float tarquetZoom)
    {
        tarquetFOV = tarquetZoom;
    }

    public void ZoomOut()
    {
        tarquetFOV = startFOV;
    }
}
