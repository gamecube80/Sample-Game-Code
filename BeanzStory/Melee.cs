using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    [Header("Time Settings")]
    public float startTimeBetweenAttack;
    private float timeBetweenAttack;
    public float startComboTime;
    private float comboTime;

    [Header("Attack Parameters")]
    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public int startDamage;
    private int damage;
    private int attackNumber;
    public float startStaminaCost;
    private float staminaCost;


    // Use this for initialization
    void Start() {
        attackNumber = 0;
    }

    // Update is called once per frame
    void Update() {
        damage = startDamage + attackNumber;
        staminaCost = Mathf.Floor(startStaminaCost + attackNumber * 2.5f);
        comboTime -= Time.deltaTime;
        timeBetweenAttack -= Time.deltaTime;
        GetComponent<MyCharacterController>().staminaRegenTime -= Time.deltaTime;

        if (comboTime < 0 && attackNumber > 0) {
            attackNumber = 0;
            //Debug.Log("Combo Reset");
        }

        //3 hit melee combo
        if ((timeBetweenAttack <= 0) && (GetComponent<MyCharacterController>().currentStamina >= staminaCost)) {
            //put attack animation here
            if (Input.GetButtonDown("Fire1")) {
                if (attackNumber == 0) {
                    //animation for first attack
                    Collider[] enemiesToDamage = Physics.OverlapSphere(attackPos.position, attackRange, whatIsEnemy);
                    for (int i = 0; i < enemiesToDamage.Length; i++) {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                        Debug.Log("Attacked");
                    }
                    attackNumber += 1;
                    comboTime = startComboTime;
                    timeBetweenAttack = startTimeBetweenAttack;
                    GetComponent<MyCharacterController>().currentStamina -= staminaCost;
                    GetComponent<MyCharacterController>().staminaRegenTime = GetComponent<MyCharacterController>().startStaminaRegenTime;
                    //Debug.Log("Attack 1: " + damage + "stamina: " + GetComponent<MyCharacterController>().currentStamina);
                }
                else if (attackNumber == 1 && comboTime > 0) {
                    //animation for second attack
                    Collider[] enemiesToDamage = Physics.OverlapSphere(attackPos.position, attackRange, whatIsEnemy);
                    for (int i = 0; i < enemiesToDamage.Length; i++) {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    }
                    attackNumber += 1;
                    comboTime = startComboTime;
                    timeBetweenAttack = startTimeBetweenAttack;
                    GetComponent<MyCharacterController>().currentStamina -= staminaCost;
                    GetComponent<MyCharacterController>().staminaRegenTime = GetComponent<MyCharacterController>().startStaminaRegenTime;
                    //Debug.Log("Attack 2: " + damage + " stamina: " + GetComponent<MyCharacterController>().currentStamina);
                }
                else if ((attackNumber == 2) && (comboTime > 0)) {
                    // animation for 3rd attack
                    Collider[] enemiesToDamage = Physics.OverlapSphere(attackPos.position, attackRange, whatIsEnemy);
                    for (int i = 0; i < enemiesToDamage.Length; i++) {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    }
                    timeBetweenAttack = 2 * startTimeBetweenAttack;
                    comboTime = 0;
                    attackNumber = 0;
                    GetComponent<MyCharacterController>().currentStamina -= staminaCost;
                    GetComponent<MyCharacterController>().staminaRegenTime = GetComponent<MyCharacterController>().startStaminaRegenTime;
                    //Debug.Log("Attack 3: " + damage + " stamina: " + GetComponent<MyCharacterController>().currentStamina);
                }
            }
        }

    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
