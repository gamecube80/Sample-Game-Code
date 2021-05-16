using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraControl : MonoBehaviour
{

    public GameObject camera;
    public bool cameraFollow;
    public bool cameraJumpCut;
    public bool freezeXPosition;
    public bool freezeYPosition;
    public float x;
    public float y;
    private Vector3 newCameraPosition = new Vector3(0, 0, 0);

    private void Start()
    {
        newCameraPosition = new Vector3(x, y, -9.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (cameraJumpCut == true)
            {
                camera.transform.position = newCameraPosition;
            }
            camera.GetComponent<CameraController>().followPlayer = cameraFollow;
            camera.GetComponent<CameraController>().freezeX = freezeXPosition;
            camera.GetComponent<CameraController>().freezeY = freezeYPosition;
        }
    }
}