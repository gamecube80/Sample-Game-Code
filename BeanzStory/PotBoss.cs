using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotBoss : MonoBehaviour
{

    MyCharacterController player;

    [Header("Move Settings")]
    public GameObject potBoss;
    public Transform potPos;
    private float speed;
    public Vector3 burner1, burner2, burner3, burner4, burner5, burner6, burner7, burner8;
    public Vector3 burnerEnd1, burnerEnd2, burnerEnd3, burnerEnd4, burnerEnd5, burnerEnd6, burnerEnd7, burnerEnd8;
    private int nextPoint;
    private Transform startPoint;
    //private Transform endPoint;
    //private Transform endPoint2;

    [Header("Health")]
    public int initialHP;
    private int currentHP;
    private bool canHitPlayer;

    [Header("Time Settings")]
    private float bossTimer;

    // Destination of our current move
    private Vector3 destination;

    // Start of our current move
    private Vector3 start;

    // Current move's progress
    private float progress = 0.0f;

    float timer;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MyCharacterController>();
        canHitPlayer = true;
        start = transform.position;
        progress = 0.0f;
        nextPoint = 1;
        currentHP = initialHP;
        PickNextPoint();
        //StartCoroutine(MoveToPosition(startPoint, endPoint, 2));
    }

    // Update is called once per frame
    void Update() {
        Vector3 lookAtPosition = player.transform.position;
        bool reached = false;

        // Update our progress to our destination
        progress += speed * Time.deltaTime;

        // Check for the case when we overshoot or reach our destination
        if (progress >= 1.0f) {
            progress = 1.0f;
            reached = true;
        }

        // Update out position based on our start postion, destination and progress.
        transform.position = (destination * progress) + start * (1 - progress);

        // If we have reached the destination, set it as the new start and pick a new random point. Reset the progress
        if (reached) {
            if (bossTimer > 0) {
                bossTimer -= Time.deltaTime;
            }
        }
        if (bossTimer <= 0) {
            start = destination;
            progress = 0.0f;
            PickNextPoint();
        }

        if (currentHP == 0) {
            Destroy(gameObject);
        }

        // picks a  burner
        if (nextPoint == 1) {
            destination = burner1;
        }
        else if (nextPoint == 2) {
            destination = burnerEnd1;
        }
        else if (nextPoint == 3) {
            destination = burner2;
        }
        else if (nextPoint == 4) {
            destination = burnerEnd2;
        }
        else if (nextPoint == 5) {
            destination = burner3;
        }
        else if (nextPoint == 6) {
            destination = burnerEnd3;
        }
        else if (nextPoint == 7) {
            destination = burner4;
        }
        else if (nextPoint == 8) {
            destination = burnerEnd4;
        }
        else if (nextPoint == 9) {
            destination = burner5;
        }
        else if (nextPoint == 10) {
            destination = burnerEnd5;
        }
        else if (nextPoint == 11) {
            destination = burner6;
        }
        else if (nextPoint == 12) {
            destination = burnerEnd6;
        }
        else if (nextPoint == 13) {
            destination = burner7;
        }
        else if (nextPoint == 14) {
            destination = burnerEnd7;
        }
        else if (nextPoint == 15) {
            destination = burner8;
        }
        else if (nextPoint == 16) {
            destination = burnerEnd8;
        }
    }

    public void PickNextPoint() {
        //first order 1-8 in order
        //second order 1, 5, 2, 6, 3, 7, 4, 8
        //third order 1, 8, 5, 4, 2, 3, 6, 2, 7

        //set boss speed and timer
        if (currentHP == 3) {
            speed = 2;
            bossTimer = 2;
            canHitPlayer = true;
        }
        if (currentHP == 2) {
            speed = 2;
            bossTimer = 1.5f;
            canHitPlayer = true;
        }
        if (currentHP == 1) {
            speed = 3;
            bossTimer = 1;
            canHitPlayer = true;
        }

        //set next point
        if (nextPoint == 16) {
            nextPoint = 1;
        }
        else {
            nextPoint += 1;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        collision.collider.GetComponent<MyCharacterController>();
        if (player != null) {
            Debug.Log("Hit Something");
            player.TakeDamage(25, potBoss.transform.position);
            canHitPlayer = false;
        }
    }

    public void TakeDamage(int dmg) {
        currentHP -= dmg;
        //Debug.Log(health);
    }
}