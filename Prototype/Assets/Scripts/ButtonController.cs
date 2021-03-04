using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//changing image when pressed
public class ButtonController : MonoBehaviour{

    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;
    public SoundManager soundManage;
    void Start() {
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update(){

        if (Time.timeScale == 0f) {
            //do nothing
        }

        else {
            if (Input.GetKeyDown(keyToPress)) {
                soundManage.PlaySound("pressSound");
                theSR.sprite = pressedImage;
                //Debug.Log(transform.position);
            }
            if (Input.GetKeyUp(keyToPress)) {
                theSR.sprite = defaultImage;
            }

        }


    }

}
