using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour {

    //set positions to move using Random.Range.
    public float radius;
    public float speed;

    private int nextPoint;
    public Vector2 point1, point2, point3, point4, point5, point6;

    private bool canAttack;
    private int attackType;

    public GameObject bossShot;
    public GameObject bossSmallShot;
    public GameObject bossShotBuildUp;
    public Transform shotPoint;

    // The point we are going around in circles
    private Vector2 basestartpoint;

    // Destination of our current move
    private Vector2 destination;

    // Start of our current move
    private Vector2 start;

    // Current move's progress
    private float progress = 0.0f;

    float timer;

    // Use this for initialization
    void Start() {
        start = transform.localPosition;
        basestartpoint = transform.localPosition;
        progress = 0.0f;
        PickNewRandomDestination();
        canAttack = true;
    }

    // Update is called once per frame
    void Update() {
        bool reached = false;

        // Update our progress to our destination
        progress += speed * Time.deltaTime;

        // Check for the case when we overshoot or reach our destination
        if (progress >= 1.0f) {
            progress = 1.0f;
            reached = true;
        }

        // Update out position based on our start postion, destination and progress.
        transform.localPosition = (destination * progress) + start * (1 - progress);

        // If we have reached the destination, set it as the new start and pick a new random point. Reset the progress
        if (reached) {
            if (timer < 2) {
                timer += Time.deltaTime;
                Debug.Log(timer);
                if (canAttack == true) {
                    Attack();
                    canAttack = false;
                }
            }
            else if (timer >= 2) {
                start = destination;
                PickNewRandomDestination();
                attackType = Random.Range(1, 3);
                canAttack = true;
                progress = 0.0f;
            }
        }
    }

    void PickNewRandomDestination() {
        // We add basestartpoint to the mix so that is doesn't go around a circle in the middle of the scene.
        nextPoint = Random.Range(1, 6);
        timer = 0;

        if (nextPoint == 1) {
            destination = point1;
        }

        else if (nextPoint == 2) {
            destination = point2;
        }

        else if (nextPoint == 3) {
            destination = point3;
        }

        else if (nextPoint == 4) {
            destination = point4;
        }

        else if (nextPoint == 5) {
            destination = point5;
        }

        else if (nextPoint == 6) {
            destination = point6;
        }

        //destination = Random.insideUnitCircle * radius + basestartpoint;
    }

    void Attack() {
        if (attackType == 1) {
            StartCoroutine(Attack1StartUp());
        }
        else if(attackType == 2) {
            StartCoroutine(Attack2StartUp());
        }
    }

    IEnumerator Attack1StartUp()
    {
        Instantiate(bossShotBuildUp, shotPoint.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        Instantiate(bossShot, shotPoint.position, transform.rotation);
    }

    IEnumerator Attack2StartUp()
    {
        Instantiate(bossShotBuildUp, shotPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.9f);
        Instantiate(bossSmallShot, shotPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(bossSmallShot, shotPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(bossSmallShot, shotPoint.position, transform.rotation);
    }
}