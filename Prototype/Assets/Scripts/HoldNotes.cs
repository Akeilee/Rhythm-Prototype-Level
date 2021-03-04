using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldNotes : MonoBehaviour
{

    public static object instance;
    public bool canBePressed;
    public KeyCode keyToPress;
    private float buttonXPos = -2.65f;
    private bool heldDown = false;
    private float heldTime = 0;
    public GameObject coolEffect, greatEffect, perfectEffect, missEffect;
    Vector3 pos = new Vector3(0, 0, 0);
    public static bool cheat = false;

    void Start()
    {

    }

    void Update(){

        if (Time.timeScale != 0f && cheat == true) {
            if (canBePressed) {
                gameObject.SetActive(false);
                GameManager.instance.PerfectHit();
                Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
            }
        }

        if (Input.GetKeyDown(keyToPress) && Time.timeScale != 0f && cheat == false) { //keeping track of when they first start holding key
            heldTime = Time.timeSinceLevelLoad;
            heldDown = false;
        }
        else if (Input.GetKeyDown(keyToPress) && cheat == false) {
            if (!heldDown) {
                //do nothing if not held down
               // Debug.Log("NOT HELD");
                gameObject.SetActive(false);
            }
            heldDown = false;
        }

        if (transform.position.x <= -8.0) {
           // gameObject.SetActive(false);
        }


        if (Input.GetKey(keyToPress) && Time.timeScale != 0f && cheat == false) {
            if (Time.timeSinceLevelLoad - heldTime >= 0.05) {

                if (canBePressed) {
                    gameObject.SetActive(false);

                    if (transform.position.x > buttonXPos + 0.321 || transform.position.x < buttonXPos - 0.321) {
                        //Debug.Log("Cool");
                        GameManager.instance.HoldCoolHit();
                        pos = new Vector3(-3.6f, transform.position.y, transform.position.z);
                        Instantiate(coolEffect, transform.position, coolEffect.transform.rotation);
                      
                    }
                    else if (transform.position.x > buttonXPos + 0.151 || transform.position.x < buttonXPos - 0.151) {
                        //Debug.Log("Great");
                        GameManager.instance.HoldGreatHit();
                        pos = new Vector3(-3.6f, transform.position.y, transform.position.z);
                        Instantiate(greatEffect, transform.position, greatEffect.transform.rotation);
                        
                    }
                    else {
                        //Debug.Log("Perfect");
                        GameManager.instance.HoldPerfectHit();
                        pos = new Vector3(-3.6f, transform.position.y, transform.position.z);
                        Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                       
                    }

                }

                heldDown = true;

            }
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                canBePressed = false; //makes them disappear
                GameManager.instance.HoldNoteMissed();
                pos = new Vector3(-3.6f, transform.position.y, transform.position.z);
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
}
