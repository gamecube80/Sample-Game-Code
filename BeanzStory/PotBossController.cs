using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotBossController : MonoBehaviour {
    [Header("Health & Stamina")]
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    [Header("Movement")]
    public float velocity;
    public float turnSpeed;

    void Move () {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
