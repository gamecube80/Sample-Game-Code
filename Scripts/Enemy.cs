#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private PlayerController player;

    [Header("Enemy Stats")]
    public int health;
    public float initialSpeed;
    private float currentSpeed;

    [Header("Take Damage Settings")]
    private float dazedTime;
    public float startDazedTime;

    [HideInInspector]public Transform myTrans;
    private Rigidbody2D myBody;
    private float myWidth;

    [Header("Detect Edge")]
    public LayerMask whatIsGround;
    public float checkRadius;
    private bool isGrounded;

    [Header("Damage Player")]
    public float knockTime;
    public float knockPwr;
    public int damage;

    [Header("Audio")]
    public AudioSource soundSource;
    public AudioClip hitSound;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        myTrans = this.transform;
        //myBody = this.GetComponent<Rigidbody2D>();
        //myWidth = this.GetComponent<Rigidbody2D>().bounds.extents.x;
        myWidth = 0.5f; //temporary until we have a sprite in place
    }

    private void FixedUpdate() {
        // detects if there is ground in front of the enemy
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, whatIsGround);
        //Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);

        // turns around if there is no ground
        if (!isGrounded) {
            Vector3 EnemyRotation = myTrans.eulerAngles;;
            EnemyRotation.y += 180;
            myTrans.eulerAngles = EnemyRotation;
        }

        transform.Translate(Vector2.left.normalized * currentSpeed * Time.deltaTime);

        if (dazedTime <= 0) {
            currentSpeed = initialSpeed;
        }
        else {
            currentSpeed = 0;
            dazedTime -= Time.deltaTime;
        }
     }

    private void OnCollisionEnter2D(Collision2D collision) {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null) {
        player.TakeDamage(damage);
        if (player.myPos.position.x < myTrans.position.x){
                StartCoroutine(player.Knockback(knockTime, knockPwr, player.transform.position));
            }
            //damage animation
        else if  (player.myPos.position.x > myTrans.position.x) {
                StartCoroutine(player.negKnockback(knockTime, knockPwr, player.transform.position));
            }
        }
     }

    public void TakeDamage(int dmg) {
        dazedTime = dmg * startDazedTime;
        soundSource.clip = hitSound;
        soundSource.Play();

        health -= dmg;
        if (health <= 0) {
            //put particle effect here Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
