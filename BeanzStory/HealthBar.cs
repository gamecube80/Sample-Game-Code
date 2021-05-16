using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public MyCharacterController myCharacterController;
    public Image fillImage;
    private Slider slider;
    public Text healthText;


    // Use this for initialization
    private void Awake() {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    public void Update() {
        float fillValue = (myCharacterController.currentHealth / myCharacterController.maxHealth);
        slider.value = fillValue;
        //healthText.text = myCharacterController.currentHealth.ToString() + "/" + myCharacterController.maxHealth.ToString();
    }
}
