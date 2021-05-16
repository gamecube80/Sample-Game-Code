using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBoomerang : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.GetComponent<Weapon>().ObtainBoomerang(); //increase current gun level by 1
            Destroy(gameObject);
        }
    }
}
