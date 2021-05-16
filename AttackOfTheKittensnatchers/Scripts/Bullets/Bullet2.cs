using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour {
    public float speed;
    public int damage;
    GameObject player;
    Direction playerDir;
    Vector2 target;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }

        playerDir = player.GetComponent<PlayerController>().PlayerDirection;
        if (playerDir == Direction.Right)
        {
            Debug.Log("Right");
            target = new Vector2(player.transform.position.x + 20, player.transform.position.y);
        }
        else if (playerDir == Direction.Left)
        {
            Debug.Log("Left");
            target = new Vector2(player.transform.position.x - 20, player.transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {//transform.position.x == target.x && transform.position.y == target.y){
            if (collision.collider.CompareTag("Enemy"))
            {
                //Debug.Log("Enemy Hit");
                collision.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            else if (collision.collider.CompareTag("FlyingEnemy"))
            {
                //Debug.Log("Enemy Hit");
                collision.collider.GetComponent<FlyingEnemy>().TakeDamage(damage);
            }
            else if (collision.collider.CompareTag("Button"))
            {
                //Debug.Log("Button Hit");
                collision.collider.GetComponent<DoorTrigger>().TakeDamage(damage);
            }
            else if (collision.collider.CompareTag("Destroyable"))
            {
                //Debug.Log("Object Hit");
                collision.collider.GetComponent<Destroyable>().TakeDamage(damage);
            }
            DestroyProjectile();
        }
    }
}
