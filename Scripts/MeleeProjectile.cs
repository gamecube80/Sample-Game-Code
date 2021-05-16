using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeProjectile : MonoBehaviour {

    public float lifeTime;

	// Use this for initialization
	void Start () {
        Invoke("DestroyProjectile", lifeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void DestroyProjectile() {
        //put particle effects here Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
