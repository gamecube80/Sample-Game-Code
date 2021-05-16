using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    public float speed;
    public int damage;
    PlayerController player;
    private Transform playerTrans;
    private Vector2 target;
    void Start() {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        target = new Vector2(playerTrans.position.x, playerTrans.position.y);
    }    
    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Hit Something!");
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player != null) {
            Debug.Log("Player hit by Projectile");
            player.TakeDamage(damage);
            DestroyProjectile();
        }
    }
    void DestroyProjectile() {
        Destroy(gameObject);
    }
    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y) {
            DestroyProjectile();
        }
    }
}
