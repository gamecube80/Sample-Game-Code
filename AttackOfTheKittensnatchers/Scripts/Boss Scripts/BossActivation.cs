using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour {
    public GameObject boss;
    public GameObject bossHP;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            boss.gameObject.SetActive(true);
            bossHP.gameObject.SetActive(true);
        }

    }
}
