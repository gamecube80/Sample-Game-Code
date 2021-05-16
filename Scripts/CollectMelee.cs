using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMelee : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.GetComponent<Weapon>().ObtainMelee(); //increase current gun level by 1
            Destroy(gameObject);
        }
    }
}
