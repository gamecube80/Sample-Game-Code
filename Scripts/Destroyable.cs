using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

    public int health;
    public GameObject destroyedEffect;
    public AudioSource soundSource;
    public AudioClip destroySound;

    public void TakeDamage(int dmg) {
        Debug.Log("Smash!");
        health -= dmg;
        if (health <= 0)
            Instantiate(destroyedEffect, transform.position, Quaternion.identity);
            soundSource.clip = destroySound;
            soundSource.Play();
            Destroy(gameObject);
    }
}
