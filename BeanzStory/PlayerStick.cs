using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStick : MonoBehaviour {

    public GameObject player;

    //makes player move with cart
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            player.transform.parent = null;
        }
    }
}
