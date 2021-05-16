using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDown : MonoBehaviour {

    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    // Use this for initialization
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Enemy")) {
                //Debug.Log("Enemy Hit");
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            else if (hitInfo.collider.CompareTag("FlyingEnemy")) {
                //Debug.Log("Enemy Hit");
                hitInfo.collider.GetComponent<FlyingEnemy>().TakeDamage(damage);
            }
            else if (hitInfo.collider.CompareTag("Button")) {
                //Debug.Log("Button Hit");
                hitInfo.collider.GetComponent<DoorTrigger>().TakeDamage(damage);
            }
            else if (hitInfo.collider.CompareTag("Destroyable")) {
                //Debug.Log("Object Hit");
                hitInfo.collider.GetComponent<Destroyable>().TakeDamage(damage);
            }

            DestroyProjectile();

        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.Translate(Vector2.down * (speed / 2) * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        //put particle effects here Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
