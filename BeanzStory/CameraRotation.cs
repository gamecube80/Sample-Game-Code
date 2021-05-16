using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    public Transform target;
    public Vector3 offsetPos;
    public float movespeed = 5;
    public float turnSpeed = 10;
    public float smoothSpeed = 0.5f;

    Quaternion targetRotation;
    Vector3 targetPos;
    bool smoothRotation = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveWithTarget();
        LookAtTarget();

        if ((Input.GetAxis("Camera Horizontal") < 0) && !smoothRotation) {
            StartCoroutine("RotateAroundTarget", 45);
        }

        if ((Input.GetAxis("Camera Horizontal") > 0) && !smoothRotation) {
            StartCoroutine("RotateAroundTarget", -45);
        }
	}

    // Move the camera to the player position + camera offset
    void MoveWithTarget() {
        targetPos = target.position + offsetPos;
        transform.position = Vector3.Lerp(transform.position, targetPos, movespeed * Time.deltaTime);
    }

    void LookAtTarget() {
        targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    IEnumerator RotateAroundTarget(float angle) {
        Vector3 vel = Vector3.zero;
        Vector3 targetOffsetPos = Quaternion.Euler(0, angle, 0) * offsetPos;
        float dist = Vector3.Distance(offsetPos, targetOffsetPos);
        smoothRotation = true;

        while( dist > 0.02f) {
            offsetPos = Vector3.SmoothDamp(offsetPos, targetOffsetPos, ref vel, smoothSpeed);
            dist = Vector3.Distance(offsetPos, targetOffsetPos);
            yield return null;
        }
        smoothRotation = false;
        offsetPos = targetOffsetPos;
    }
}
