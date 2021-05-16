using System.Collections;
using UnityEngine;

public class PintoAnimController : MonoBehaviour {

    public Animator pintoControl;

	// Use this for initialization
	void Start () {
        pintoControl = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            pintoControl.SetBool("running", true);
            pintoControl.SetInteger("condition", 1);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            pintoControl.SetBool("running", false);
            pintoControl.SetInteger("condition", 0);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            pintoControl.SetBool("attacking", true);
            pintoControl.SetInteger("condition", 2);
            StartCoroutine(AttackRoutine());
            pintoControl.SetInteger("condition", 0);
            pintoControl.SetBool("attacking", false);          
        }
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(1);
    }
}
