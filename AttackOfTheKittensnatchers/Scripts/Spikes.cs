#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private PlayerController player;

    public int damage;
    private Transform myTrans;

    public float knockTime;
    public float knockPwr;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        myTrans = this.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null) {
            player.TakeDamage(damage);
            if (player.myPos.position.x > myTrans.position.x) {
                StartCoroutine(player.Knockback(knockTime, knockPwr, player.transform.position));
            }
            //damage animation
            else if (player.myPos.position.x < myTrans.position.x) {
                StartCoroutine(player.negKnockback(knockTime, knockPwr, player.transform.position));
            }
        }
    }
}
