using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour {

    public Boss boss;
    public Image fillImage;
    private Slider slider;
    //public Text healthText;

    // Use this for initialization
    private void Awake() {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    public void Update() {
        float fillValue = (boss.health / boss.startingHealth);
        slider.value = fillValue;
        //healthText.text = myCharacterController.currentHealth.ToString() + "/" + myCharacterController.maxHealth.ToString();
        if(boss.health <= 0) {
            Destroy(slider);
            Destroy(fillImage);
        }
    }
}
