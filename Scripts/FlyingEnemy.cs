using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

    private PlayerController player;

    [Header("Enemy Stats")]
    public int health;
    public float initialSpeed;
    private float currentSpeed;

    [Header("Take Damage Settings")]
    private float dazedTime;
    public float startDazedTime;

    [HideInInspector] public Transform myTrans;
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

    private float moveLimitL;
    private float moveLimitR;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        myTrans = this.transform;
        moveLimitL = myTrans.transform.position.x - 5;
        moveLimitR = myTrans.transform.position.x + 5;
        myWidth = 0.5f;

    }
    private void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        if (myTrans.transform.position.x <= moveLimitL || myTrans.transform.position.x >= moveLimitR)
        {
            Vector3 EnemyRotation = myTrans.eulerAngles; ;
            EnemyRotation.y += 180;
            myTrans.eulerAngles = EnemyRotation;
        }
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        if (dazedTime <= 0)
        {
            currentSpeed = initialSpeed;
        }
        else
        {
            currentSpeed = 0;
            dazedTime -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
            if (player.myPos.position.x < myTrans.position.x)
            {
                StartCoroutine(player.Knockback(knockTime, knockPwr, player.transform.position));
            }
            //damage animation
            else if (player.myPos.position.x > myTrans.position.x)
            {
                StartCoroutine(player.negKnockback(knockTime, knockPwr, player.transform.position));
            }
        }
    }
    public void TakeDamage(int dmg)
    {
        dazedTime = dmg * startDazedTime;
        health -= dmg;
        if (health <= 0)
            //put particle effect here Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
    }
}
