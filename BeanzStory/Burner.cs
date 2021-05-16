using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour {

	public GameObject flame;
	private bool isActive;
	public int health = 1;
	public float timer;

	public Transform burnerLocation;

	// Use this for initialization
	void Start () {
		isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			timer -= Time.deltaTime;
			if(timer <= 0) {
				isActive = false;
				health = 1;
			}
		}
	}

	public void TakeDamage(int dmg) {
		health -= dmg;
		if (health <= 0) {
			isActive = true;
			Instantiate(flame, burnerLocation.position, transform.rotation);
			Debug.Log("flame Triggered");
			timer = 2.0f;
		}
	}
}
