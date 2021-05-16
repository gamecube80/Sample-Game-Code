using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour {

    public float speed;
    public int damage;

    void Start() {

    }

    void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void DestroyProjectile() {
        Destroy(gameObject);
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
}
