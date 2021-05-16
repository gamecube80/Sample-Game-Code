using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrigger : MonoBehaviour {

	public MyCharacterController player;
	public Canvas canvas;

	// Use this for initialization
	void Start () {
		canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			canvas.enabled = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			canvas.enabled = false;
		}
	}
}
