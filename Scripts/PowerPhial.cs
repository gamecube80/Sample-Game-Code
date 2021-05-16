using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPhial : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<Weapon>().PowerUpCurrentGun(); //increase current gun level by 1
            Destroy(gameObject);
        }
    }
}
