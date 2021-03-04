using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour{

    public AudioClip clickSound;
    public AudioClip hoverSound;
    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource musicSource { get { return GetComponent<AudioSource>(); } }

    private Button button2 { get { return GetComponent<Button>(); } }
    private AudioSource musicSource2 { get { return GetComponent<AudioSource>(); } }

    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        musicSource.clip = clickSound;
        musicSource.playOnAwake = false;
        button.onClick.AddListener(() => PlaySound());


        musicSource2.clip = hoverSound;
        musicSource2.playOnAwake = false;
        button2.onClick.AddListener(() => HoverSound());
    }


    void Update()
    {
        
    }

    void PlaySound() {
        musicSource.PlayOneShot(clickSound);
    }

    public void HoverSound() {
        musicSource2.PlayOneShot(hoverSound);
    }
}
