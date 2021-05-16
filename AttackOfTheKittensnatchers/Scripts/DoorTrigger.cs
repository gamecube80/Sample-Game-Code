#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    public GameObject door;
    private bool isOpened = false;
    public int health;

    // when hit, the button is destroyed and door is opened
    public void TakeDamage(int dmg) {
        health -= dmg;
        if (health <= 0) {
            door.transform.position += new Vector3(0, 4, 0);
            //Debug.Log("Button Shot");
            isOpened = true;
            Destroy(gameObject);
        }
    }
}
