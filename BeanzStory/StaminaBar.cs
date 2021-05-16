using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour {

    public MyCharacterController myCharacterController;
    public Image fillImage;
    private Slider slider;
    public Text staminaText;


    // Use this for initialization
    private void Awake() {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    public void Update() {
        float fillValue = myCharacterController.currentStamina / myCharacterController.maxStamina;
        slider.value = fillValue;
        //staminaText.text = myCharacterController.currentStamina.ToString() + "/" + myCharacterController.maxStamina.ToString();
    }
}
