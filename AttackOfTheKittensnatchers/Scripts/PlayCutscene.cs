using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCutscene : MonoBehaviour {

    public Animator Abduction;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player")) {
            if (GameObject.Find("Player").GetComponent<PlayerController>()) {
                GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
                GameObject.Find("Player").GetComponent<PlayerController>().rb.velocity = Vector3.zero;
            }

            Abduction.SetBool("CutsceneTrigger", true);
            StartCoroutine(ExecuteAfterTime()); 
        }
    }

    IEnumerator ExecuteAfterTime() {
        yield return new WaitForSeconds(10);

        if (GameObject.Find("Player").GetComponent<PlayerController>()) {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;

            Destroy(gameObject);
        }
    }
}
