using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang2 : MonoBehaviour {
    public float speed;
    public int damage;
    GameObject player;
    Direction playerDir;
    Vector2 target;
    public float deceleration;
    public float maxDecel;
    public float lifeTime;
    float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerDir = player.GetComponent<PlayerController>().PlayerDirection;
        if (playerDir == Direction.Right)
        {
            //Debug.Log("Right");
            target = new Vector2(player.transform.position.x + 20, player.transform.position.y);
        }
        else if (playerDir == Direction.Left)
        {
            //Debug.Log("Left");
            target = new Vector2(player.transform.position.x - 20, player.transform.position.y);
        }
        timer = lifeTime + Time.deltaTime;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (speed <= maxDecel)
        {
            speed = maxDecel;
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        speed = speed - deceleration;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hit");
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit");
            collider.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
        else if (collider.CompareTag("FlyingEnemy"))
        {
            Debug.Log("Enemy Hit");
            collider.GetComponent<FlyingEnemy>().TakeDamage(damage);
            DestroyProjectile();
        }
        else if (collider.CompareTag("Button"))
        {
            Debug.Log("Button Hit");
            collider.GetComponent<DoorTrigger>().TakeDamage(damage);
            DestroyProjectile();
        }
        else if (collider.CompareTag("Destroyable"))
        {
            Debug.Log("Object Hit");
            collider.GetComponent<Destroyable>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
