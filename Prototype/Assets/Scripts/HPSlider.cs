using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour {

    public Slider hpbar;
    public int maxHealth = 500;
    public int currentHealth;
    public Text hpText;
    void Start() {
        currentHealth = maxHealth;
        hpbar.maxValue = maxHealth;
        hpbar.value = maxHealth;
        hpText.text = currentHealth + "/" + maxHealth;
    }
    void Update() {
        hpbar.value = currentHealth;
        hpText.text = currentHealth + "/" + maxHealth;

        if (currentHealth <= 0) {
            hpText.text = "0" + "/" + maxHealth;
            //Debug.Log("Game Over");
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
    }
}
