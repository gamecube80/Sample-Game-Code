using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private MyCharacterController player;

    [Header("Enemy Stats")]
    public int maxHealth;
    public int speed;
    public int damage;
    public Transform myTrans;
    private Rigidbody rb;

    [HideInInspector]
    public float currentHealth;

    [Header("Knockback")]
    public float knockTime;
    public float knockPwr;

    [Header("Attack Parameters")]
    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    //private float startTimeBeforeAttack;
    private float timeBeforeAttack;

    private Transform target;
    private Animator enemyAnimControl;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MyCharacterController>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyAnimControl = this.GetComponent<Animator>();
        currentHealth = maxHealth;
        timeBeforeAttack = Random.Range(1, 3);
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        float dist = Vector3.Distance(target.position, transform.position);

        if (dist < 25) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            Vector3 lookAtPosition = target.position;
            lookAtPosition.y = transform.position.y;
            transform.LookAt(lookAtPosition);
            enemyAnimControl.SetInteger("condition", 1);
        }
        if (dist <= 2) {
            if (timeBeforeAttack <= 0) {
                MeleeAttack();
                timeBeforeAttack = Random.Range(1, 3); ;
            }
            else {
                enemyAnimControl.SetInteger("condition", 0);
                timeBeforeAttack -= Time.deltaTime;
                //Debug.Log(timeBeforeAttack);
            }
        }
        else {
            timeBeforeAttack = Random.Range(1, 3); ;
        }
    }

    public void TakeDamage(int dmg) {
        currentHealth -= dmg;
        Debug.Log("Took damage");
        Debug.Log(currentHealth);
        StartCoroutine(Knockback(knockTime, knockPwr, player.myPos.transform.position - myTrans.transform.position));
        if (currentHealth <= 0) {
            //put particle effect here Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void MeleeAttack() {
        //Debug.Log("Attack");
        timeBeforeAttack = Random.Range(1, 2);
        enemyAnimControl.SetInteger("condition", 2);
        //put attack animation here
        Collider[] damagePlayer = Physics.OverlapSphere(attackPos.position, attackRange, whatIsEnemy);
        Vector3 hitDirection = myTrans.position;
        player.TakeDamage(damage, hitDirection);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public IEnumerator Knockback(float knockDur, float knockPwr, Vector3 knockDir) {

        float timer = 0;

        while (knockDur > timer) {

            timer += Time.deltaTime;

            rb.AddForce(new Vector3(knockDir.normalized.x * -200, knockDir.normalized.y * knockPwr, transform.position.z));
            rb.AddForce(new Vector3(transform.position.x, knockDir.normalized.y * knockPwr, knockDir.normalized.z * -200));
        }

        yield return 0;
    }
}