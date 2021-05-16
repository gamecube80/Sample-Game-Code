using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{

    private Enemy enemy;

    [Header("Health & Stamina")]
    public int maxHealth;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentStamina;
    public float maxStamina;
    public float staminaRegen;
    [HideInInspector] public float staminaRegenTime;
    public float startStaminaRegenTime;
    public float Iframes = 0.36f;

    [Header("Movement")]
    public Transform myPos;
    private Rigidbody rb;
    public float velocity;
    public float turnSpeed;

    [Header("Knockback")]
    public float knockTime;
    public float knockPwr;

    [Header("Jump Settings")]
    public CapsuleCollider radius;
    public LayerMask whatIsGround;
    public float jumpForce;
    public float jumpStamCost;

    Vector2 input;
    float angle;

    Quaternion targetRotation;
    Transform cam;



    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        radius = GetComponent<CapsuleCollider>();

        cam = Camera.main.transform;
        currentStamina = maxStamina;
        currentHealth = maxHealth;
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }


    // Update is called once per frame
    void Update() {
        if (staminaRegenTime < 0) {
            currentStamina += staminaRegen;
        }

        if (currentStamina > maxStamina) {
            currentStamina = maxStamina;
        }

        if (IsGrounded() && Input.GetButtonDown("Jump")) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            currentStamina -= jumpStamCost;
        }

        GetInput();

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

        CalculateDirection();
        Rotate();
        Move();


    }

    // Movement input based on arrows and wasd
    void GetInput() {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    // Direction Relative to Camera Rotation
    void CalculateDirection() {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    // Rotate toward calculated angle
    void Rotate() {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    // Moves player along its own forward axis
    void Move() {

        transform.position += transform.forward * velocity * Time.deltaTime;
    }

    public void TakeDamage(int dmg, Vector3 hitDirection) {
        currentHealth -= dmg;
        hitDirection -= transform.position;
        //Debug.Log(currentHealth);
        StartCoroutine(PlayerKnockback(knockTime, knockPwr, hitDirection));
        StartCoroutine(InvulnerableTime(Iframes));
        if (currentHealth <= 0) {
            //put particle effect here Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private bool IsGrounded() {
        return Physics.CheckCapsule(radius.bounds.center, new Vector3(radius.bounds.center.x, radius.bounds.min.y, radius.bounds.center.z),
            radius.radius * .75f, whatIsGround);
    }

    public IEnumerator PlayerKnockback(float knockDur, float knockPwr, Vector3 knockDir) {

        float timer = 0;

        while (knockDur > timer) {

            timer += Time.deltaTime;

            rb.AddForce(new Vector3(knockDir.normalized.x * -200, knockDir.normalized.y * knockPwr, transform.position.z));
            rb.AddForce(new Vector3(transform.position.x, knockDir.normalized.y * knockPwr, knockDir.normalized.z * -200));
            rb.AddForce(new Vector3(transform.position.x, knockDir.normalized.y * 200, transform.position.z));
        }

        yield return 0;
    }

    IEnumerator InvulnerableTime(float Iframes) {
        // turn off collision with enemies
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics.IgnoreLayerCollision(enemyLayer, playerLayer);

        // wait for Iframes
        yield return new WaitForSeconds(Iframes);

        // re-enable collision
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
    }
}