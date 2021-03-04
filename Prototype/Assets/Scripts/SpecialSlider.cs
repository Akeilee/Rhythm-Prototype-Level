using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialSlider : MonoBehaviour{

    public GameManager GM;
    private float maxValue = 28;
    public Slider slider;
    public GameObject special;
    public GameObject success;
    public GameObject fail;
    void Start() {
        slider.value = 0;
        slider.maxValue = maxValue;
        special.SetActive(false);
    }

    void Update() {
        slider.maxValue = maxValue;

        if (GM.timecount >= 85) {
            special.SetActive(true);
            slider.value = GM.specialCounter;
        }

        if (GM.timecount >= 101) {
            special.SetActive(false);
        }

        if (GM.timecount >= 101 && GM.specialCounter >= 20) {
            success.SetActive(true);
        }
        if (GM.timecount >= 101 && GM.specialCounter < 20) {
            fail.SetActive(true);
        }

        if (GM.timecount >= 103) {
            success.SetActive(false);
            fail.SetActive(false);
        }
    }
}
