using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointActivation : MonoBehaviour {

    public Animator animator;
    public bool active = false;
    private bool currentlyActive = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (active == true && currentlyActive == false)
        {
            currentlyActive = false;
            animator.SetBool("ActiveCheckPoint", true);
        }
	}
}
