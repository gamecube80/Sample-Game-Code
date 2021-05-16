using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left, Right };
public class PlayerController : MonoBehaviour {

    [Header("Movement Settings")]
    public Transform myPos;
    [HideInInspector] public Rigidbody2D rb;
    public float speed;
    private float originalSpeed;
    private float moveInput;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private float dashCooldown;
    public float startDashCooldown;
    private bool canDash;

    [Header("Jump Settings")]
    public Transform feetPos;
    private bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpForce;
    public float originalJumpForce;
    public float jumpTime;
    private bool isJumping;
    private bool groundCheck;

    [Header("Health and Damage")]
    public float startIFrames;
    private bool canBeHit;
    private float Iframes;
    public int initialPlayerHealth;
    private int currentPlayerHealth;
    public Transform currentSpawn;

    [Header("Audio")]
    public AudioSource bgmSource;
    public AudioSource soundSource;
    public AudioClip bgm;
    public AudioClip bossBgm;
    public AudioClip dashSound;
    public AudioClip powerUpSound;
    public AudioClip jumpSound;

    [Header("Animation and Effects")]
    public Animator animator;
    public Animator steamAnimator;
    public GameObject dashRightEffect;
    public GameObject dashLeftEffect;
    public GameObject powerUpEffect;
    public GameObject victoryScreen;
    public bool victory = false;
    private bool playingVictory = false;


    private Direction playerDirection = Direction.Right;

    public Direction PlayerDirection {
        get {
            return playerDirection;
        }
    }

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        myPos = this.transform;

        currentPlayerHealth = initialPlayerHealth;

        bgmSource.clip = bgm;
        bgmSource.Play();

        originalSpeed = speed;
        originalJumpForce = jumpForce;

        dashTime = startDashTime;
    }

    // Use to update physics related objects
    void FixedUpdate() {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // This code allows the player to slow down and come to a stop quicker without sliding. -DS
        if (moveInput <= 0.4 && moveInput >= -0.4) {
            rb.velocity = new Vector2(rb.velocity.x / 3, rb.velocity.y);
        }

        else if (Input.GetButtonDown("Dash") && dashCooldown <= 0) {
            if (dashTime > 0 && rb.velocity.x > 0) {
                //Debug.Log("Dash");
                soundSource.clip = dashSound;
                soundSource.Play();
                dashTime -= Time.deltaTime;
                rb.velocity = new Vector2(moveInput * dashSpeed, rb.velocity.y);
                Instantiate(dashRightEffect, transform.position, Quaternion.identity);
                dashCooldown = startDashCooldown;
            }
            else if (dashTime > 0 && rb.velocity.x < 0) {
                //Debug.Log("Dash");
                soundSource.clip = dashSound;
                soundSource.Play();
                dashTime -= Time.deltaTime;
                rb.velocity = new Vector2(moveInput * dashSpeed, rb.velocity.y);
                Instantiate(dashLeftEffect, transform.position, Quaternion.identity);
                dashCooldown = startDashCooldown;
            }
        }

        if (dashCooldown > 0) {
            dashCooldown -= Time.deltaTime;
        }
        else if (dashCooldown <= 0) {
            dashTime = startDashTime;
        }
    }

    // Update is called once per frame
    void Update() {
        this.GetComponent<HealthDisplay>().health = currentPlayerHealth;
        steamAnimator.SetInteger("Health", currentPlayerHealth);

        if (moveInput > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0); //flip character right while facing right
            playerDirection = Direction.Right;
        }
        else if (moveInput < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0); //flip character left if facing left
            playerDirection = Direction.Left;
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true && groundCheck == true) {
            StartCoroutine(JumpAnimationCheck());
        }

        if (isGrounded == true && Input.GetButtonDown("Jump")) {
            soundSource.clip = jumpSound;
            soundSource.Play();
            isJumping = true;
            groundCheck = true;
            animator.SetBool("IsJumping", true);
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping == true) {

            if (jumpTimeCounter > 0) {
                soundSource.clip = jumpSound;
                soundSource.Play();
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
        }

        //When Player is moving left or right, play Run animation
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        if (Input.GetButtonDown("Fire1")) {
            StartCoroutine(GunShootAnimation());
        }

        //Victory
        if (victory == true)
        {
            moveInput = 0;
            if (playingVictory == false)
            {
                StartCoroutine(Victory());
            }
        }
    }

    public void TakeDamage(int dmg) {
        if (currentPlayerHealth > 0) {
            GetComponent<Weapon>().WeakenCurrentGun(); // reduce current gun level by 1
            currentPlayerHealth -= dmg;
            Iframes = startIFrames;
            animator.SetTrigger("IsHurt");
            StartCoroutine(InvulnerableTime(Iframes));
            Debug.Log("Player Hit " + currentPlayerHealth);
        }

        if (currentPlayerHealth <= 0) {
            GetComponent<Weapon>().gunPoweredUp = 0; // set gun level to 0
            GetComponent<Weapon>().boomerangPoweredUp = 0; // set boomerang level to 0
            GetComponent<Weapon>().meleePoweredUp = 0; // set melee level to 0
            StartCoroutine(Death());
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PowerPhial") {
            GameObject powerupParticle = Instantiate(powerUpEffect, transform.position, Quaternion.identity);
            powerupParticle.transform.parent = gameObject.transform;
            soundSource.clip = powerUpSound;
            soundSource.Play();
        }

        if (other.tag == "BossTrigger")
        {
            bgmSource.clip = bossBgm;
            bgmSource.Play();
        }
    }


    public IEnumerator Knockback(float knockDur, float knockPwr, Vector3 knockDir) {

        float timer = 0;

        if (currentPlayerHealth > 0)
        {
            while (knockDur > timer)
            {

                timer += Time.deltaTime;

                rb.AddForce(new Vector3(knockDir.normalized.x * -200, knockDir.normalized.y * knockPwr, transform.position.z));

            }
            yield return 0;
        }
    }

    public IEnumerator negKnockback(float knockDur, float knockPwr, Vector3 knockDir) {

        float timer = 0;
        if (currentPlayerHealth > 0)
        {
            while (knockDur > timer)
            {

                timer += Time.deltaTime;

                rb.AddForce(new Vector3(knockDir.normalized.x * 200, knockDir.normalized.y * knockPwr, transform.position.z));

            }
            yield return 0;
        }
    }

    IEnumerator InvulnerableTime(float Iframes) {
        // turn off collision with enemies
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);

        animator.SetBool("IsHurt", true);
        speed = 0;
        jumpForce = 0;
        yield return new WaitForSeconds(0.36f);
        animator.SetBool("IsHurt", false);
        speed = originalSpeed;
        jumpForce = originalJumpForce;
        // wait for Iframes
        yield return new WaitForSeconds(Iframes);

        // re-enable collision
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
    }

    IEnumerator JumpAnimationCheck() {
        yield return new WaitForSeconds(0.02f);
        if (isGrounded == true) {
            animator.SetBool("IsJumping", false);
            groundCheck = false;
        }

    }

    IEnumerator GunShootAnimation() {
        animator.SetBool("IsShootingGun", true);
        yield return new WaitForSeconds(0.36f);
        animator.SetBool("IsShootingGun", false);
    }

    public void setSpawn(Transform newSpawnPoint) {
        currentSpawn.position = newSpawnPoint.position;
    }

    public void dash(float x, float y) {
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(x, y).normalized * 100;
    }

    IEnumerator Victory()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(2f);
        animator.SetBool("VictoryAnimation", true);
        yield return new WaitForSeconds(2f);
        victoryScreen.gameObject.SetActive(true);
    }

    IEnumerator Death()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
        rb.velocity = Vector2.zero;
        bgmSource.Stop();
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(0.8f);
        bgmSource.clip = bgm;
        bgmSource.Play();
        if (GameObject.Find("BossFight") != null)
        {
            GameObject.Find("Boss").GetComponent<Boss>().health = 100;
            GameObject.Find("BossFight").SetActive(false);
            GameObject.Find("EnemyHP").SetActive(false);
        }
        rb.velocity = Vector2.zero;
        rb.transform.position = currentSpawn.transform.position;
        currentPlayerHealth = initialPlayerHealth;
        animator.SetBool("IsDead", false);
        Debug.Log(rb.position.x);
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
    }
}