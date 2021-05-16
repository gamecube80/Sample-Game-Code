using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{

    public Transform cam;
    public Enemy enemy;
    public Image fillImage;

    public Slider slider;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    public void Update() {
        float fillValue = enemy.currentHealth / enemy.maxHealth;
        //slider.value = fillValue;
        if (Input.GetKeyDown(KeyCode.Space)) {
            enemy.TakeDamage(10);
        }
    }

    private void LateUpdate() {
        transform.LookAt(transform.position + cam.forward);
    }
}