using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform mainCamera;
    void Start()
    {
        mainCamera = FindObjectOfType<CameraMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}
