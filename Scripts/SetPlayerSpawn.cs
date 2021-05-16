#pragma warning disable 0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerSpawn : MonoBehaviour {

    private PlayerController player;

    public GameObject checkPoint;
    public Transform spawnPoint;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            collider.GetComponent<PlayerController>().setSpawn(spawnPoint);
            Debug.Log("Spawn set");
            checkPoint.GetComponent<CheckpointActivation>().active = true;
            Destroy(gameObject);
        }
    }
}
