using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    private PlayerController player;

    [Header("Enemy Stats")]
    public float startingHealth;
    public float health;

    [HideInInspector] public Transform myTrans;
    private Rigidbody2D myBody;

    [Header("Damage Player")]
    public float knockTime;
    public float knockPwr;
    public int damage;

    [Header("Effects")]
    public GameObject smallExplosion;
    public GameObject largeExplosion;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        myTrans = this.transform;
        health = startingHealth;
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter2D(Collision2D collision) {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null) {
            player.TakeDamage(damage);
            if (player.myPos.position.x < myTrans.position.x) {
                StartCoroutine(player.Knockback(knockTime, knockPwr, player.transform.position));
            }
            //damage animation
            else if (player.myPos.position.x > myTrans.position.x) {
                StartCoroutine(player.negKnockback(knockTime, knockPwr, player.transform.position));
            }
        }
    }

    public void TakeDamage(int dmg) {
        health -= dmg;
        if (health <= 0) {
            GameObject.Find("Player").GetComponent<PlayerController>().victory = true;
            StartCoroutine(BossDeath());
            Destroy(gameObject);
        }
    }

    IEnumerator ExecuteAfterTime() {
        float waitTime = Random.Range(3.0f, 5.0f);
        yield return new WaitForSeconds(waitTime);

        int attackNumber = Random.Range(1, 3);

    }

    IEnumerator BossDeath()
    {
        Instantiate(smallExplosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.02f);
        Instantiate(largeExplosion, transform.position, Quaternion.identity);
    }
}
