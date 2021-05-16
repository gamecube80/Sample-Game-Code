using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour {

    private PlatformEffector2D effector;
    public float initialWaitTime;
    private float waitTime;

	// Use this for initialization
	void Start () {
        effector = GetComponent<PlatformEffector2D>();
    }
	
	// Update is called once per frame
	void Update () {

        // pass through platform
        if (Input.GetAxis("Vertical") < 0) {
            if (waitTime <= 0) {
                effector.rotationalOffset = 180f;
                waitTime = initialWaitTime;
            }
            else {
                waitTime -= Time.deltaTime;
            }
        }

        // reset effector if jumping
        if (Input.GetButtonDown("Jump")) {
            effector.rotationalOffset = 0;
        }
	}
}
