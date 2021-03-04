using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//moving notes according to bpm
public class BeatScroller : MonoBehaviour{

    public float beatTempo;
    public bool started;

    public KeyCode keyToPress;
    public bool createNotesMode;
    public GameObject noteImage;
    public float timer;
    public Vector3 pos;
    void Start(){
        beatTempo = beatTempo / 60f; //how fast to move per second
    }

    void Update(){
        timer = Time.deltaTime;
  
        if (!started)
        {
            //if (Input.anyKeyDown)
           // {
             //   hasStarted = true;
            //}
        }
        else {

            if (timer <= 0) {
                pos = transform.position;
            }

            transform.position -= new Vector3(beatTempo * timer, 0f, 0f); //moving on X axis;
        }


        //creating notes by pressing 'space'
        if (createNotesMode)
        {
           if (Input.GetKeyDown(keyToPress))
          {
            //  Instantiate(noteImage, transform.position, Quaternion.identity);
          }
        }


    }
}
