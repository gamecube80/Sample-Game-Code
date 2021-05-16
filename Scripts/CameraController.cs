using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;         //Public variable to store a reference to the player game object.
    public bool followPlayer = false; //Public variable that allows the camera to remain stationary or move with the player.
    public bool freezeX = false;
    public bool freezeY = false;

    public Vector3 offset;            //Private variable to store the offset distance between the player and camera.

    // Use this for initialization
    void Start() {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // LateUpdate is called after Update each frame
    void LateUpdate() {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (followPlayer == true)
        {
            if (freezeX == false)
            {
                transform.position = new Vector3(player.transform.position.x + offset[0], transform.position.y, offset[2]);
            }

            if (freezeY == false)
            {
                transform.position = new Vector3(transform.position.x, player.transform.position.y + offset[1], offset[2]);
            }
        }
    }
}