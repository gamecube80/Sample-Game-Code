using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject projectile;
    public GameObject projectileUp;
    public GameObject projectileDown;
    public GameObject projectileLarge;
    public Transform shotPoint;
    public bool poweredUp = false;

    public AudioSource gunSoundSource;
    public AudioClip gunSoundNormal;
    public AudioClip gunSoundPowered;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	void Update () {
        if (timeBetweenShots <=0){
            if (Input.GetButtonDown("Fire1") && poweredUp == true) //If C is pressed and the Player's Weapon is Powered Up, fire 3 projectiles.
            {
                gunSoundSource.clip = gunSoundPowered;
                gunSoundSource.Play();
                Instantiate(projectileLarge, shotPoint.position, transform.rotation);
                Instantiate(projectileUp, shotPoint.position, transform.rotation);
                Instantiate(projectileDown, shotPoint.position, transform.rotation);
                timeBetweenShots = startTimeBetweenShots;
            }
            if (Input.GetButtonDown("Fire1") && poweredUp == false) //If C is pressed and the Player's Weapon is NOT Powered Up, fire 1 standard projectile.
            {
                gunSoundSource.clip = gunSoundNormal;
                gunSoundSource.Play();
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBetweenShots = startTimeBetweenShots;
            }
        }
        else {
            timeBetweenShots -= Time.deltaTime;
        }
        
	}
}
