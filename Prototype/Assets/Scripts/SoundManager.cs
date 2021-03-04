using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

    public AudioClip playerHitSound, enemyHitSound, playerDeathSound, introSound, victorySound, specialSound, pressSound, startPanelSound;
    private AudioSource audioSource { get { return GetComponent<AudioSource>(); } }
    void Start(){

/*        playerHitSound = Resources.Load<AudioClip>("playerHitSound");
        enemyHitSound = Resources.Load<AudioClip>("enemyHitSound");
        playerDeathSound = Resources.Load<AudioClip>("playerDeathSound");
        introSound = Resources.Load<AudioClip>("introSound");
        victorySound = Resources.Load<AudioClip>("victorySound");
        specialSound = Resources.Load<AudioClip>("specialSound");*/

    }

    void Update(){
        
    }

    public void PlaySound (string clip) {
        switch (clip) {
            case "playerHitSound":
                audioSource.PlayOneShot(playerHitSound);
                break;
            case "enemyHitSound":
                audioSource.PlayOneShot(enemyHitSound);
                break;
            case "playerDeathSound":
                audioSource.PlayOneShot(playerDeathSound);
                break;
            case "introSound":
                audioSource.PlayOneShot(introSound);
                break;
            case "victorySound":
                audioSource.PlayOneShot(victorySound);
                break;
            case "specialSound":
                audioSource.PlayOneShot(specialSound);
                break;
            case "pressSound":
                audioSource.PlayOneShot(pressSound);
                break;
            case "startPanelSound":
                audioSource.PlayOneShot(startPanelSound);
                break;
        }

    }
}
