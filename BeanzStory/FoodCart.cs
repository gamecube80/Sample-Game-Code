using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCart : MonoBehaviour {

    [SerializeField]
    Vector3 v3Force;
    [SerializeField]
    float duration;
    [SerializeField]
    float durationDivsor;

    Animator animator;
    //Rigidbody rigidbody;

    bool isSleeping;

    // Use this for initialization
    void Start () {
        animator = GetComponentInParent<Animator>();
        //rigidbody = GetComponentInParent<Rigidbody>();
        isSleeping = true;
	}
	
	private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            animator.SetTrigger("TriggerMove");
            isSleeping = false;
            int i = 0;
            while (i <= duration)
            {
                duration -= durationDivsor;
                ++i;
            }
        }
    }

    void FixedUpdate()
    {
        //Debug.Log("FoodCart isSleeping " + isSleeping);
        if (duration > 0)
        {
            //rigidbody.velocity += v3Force;
        }
        else if(!isSleeping && duration < 0)
        {
            //rigidbody.Sleep();
            isSleeping = true;
        }
    }
}
