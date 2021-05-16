using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    float cooldown;
    public float shootCounter = 2;
    public GameObject projectile;
    Transform self;
    void Start()
    {
        cooldown = shootCounter;
        self = GetComponentInParent<Enemy>().myTrans; //change Enemy to FlyingEnemy if need be
    }
    private void Update()
    {
        self = GetComponentInParent<Enemy>().myTrans; //change Enemy to FlyingEnemy if need be
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player Detected!");
            if (cooldown <= 0)
            {
                Instantiate(projectile, self.transform.position, Quaternion.identity);
                cooldown = shootCounter;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
}


