using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour {

	PotBoss enemy;
	MyCharacterController player;

	public int damageToPlayer;
	public int damageToBoss;
	public float lifeTime;

	private bool enemyCanBeHit;
	private bool playerCanBeHit;

	// Use this for initialization
	void Start() {
		Invoke("DestroyProjectile", lifeTime);
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<MyCharacterController>();
		enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PotBoss>();
		enemyCanBeHit = true;
		playerCanBeHit = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Enemy") {
			enemy.TakeDamage(1);
		}
	}

	void DestroyProjectile() {
		//put particle effects here Instantiate(hitEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
