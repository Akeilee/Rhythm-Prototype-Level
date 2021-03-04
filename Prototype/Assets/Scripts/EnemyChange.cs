using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
using UnityEngine.Experimental.U2D.Animation;

public class EnemyChange : MonoBehaviour {

    public Animator birbAnimator;
    public Animator birbAnimatorG;
    public Animator birbAnimatorR;
    public Animator dodoAnimator;
    public Animator playerAnimator;
    public Animator slashPAnimator;
    public Animator slashEAnimator;
    public Animator slashDAnimator;

    //public SpriteResolver theSR;
    public GameManager GM;
    public GameObject birbObjectYellow;
    public GameObject birbObjectRed;
    public GameObject birbObjectGreen;
    public GameObject dodoObject;
    public GameObject playerObject;

    public MusicSlider musicSlide;
    public int startN;

    void Start() {
        startN = 1;
        birbObjectYellow.SetActive(false);
        birbObjectRed.SetActive(false);
        birbObjectGreen.SetActive(false);
        dodoObject.SetActive(false);

        //theSR.SetCategoryAndLabel("BirdBody", "yellow");
    }


    void Update() {

        anim();
    }

    public void anim() {

        if (musicSlide.timeRemaining <= 3) {
            birbObjectYellow.SetActive(true);
            birbAnimator.SetBool("Walk", true);
        }
        if (musicSlide.timeRemaining > 3) {
            startN = 0;
            birbAnimator.SetBool("Walk", false);
            //birbAnimator.SetBool("isIdle", true);
        }

    }


    public void GetHit() {
        if (birbObjectYellow.activeInHierarchy == true) {
            birbAnimator.SetBool("Hit", true);
            birbAnimator.SetBool("isIdle", false);
            birbAnimator.SetBool("attacking", false);
            birbAnimator.SetBool("isDead", false);
        }
        if (birbObjectGreen.activeInHierarchy == true) {
            birbAnimatorG.SetBool("Hit", true);
            birbAnimatorG.SetBool("isIdle", false);
            birbAnimatorG.SetBool("attacking", false);
            birbAnimatorG.SetBool("isDead", false);
        }
        if (birbObjectRed.activeInHierarchy == true) {
            birbAnimatorR.SetBool("Hit", true);
            birbAnimatorR.SetBool("isIdle", false);
            birbAnimatorR.SetBool("attacking", false);
            birbAnimatorR.SetBool("isDead", false);
        }
        if (dodoObject.activeInHierarchy == true) {
            dodoAnimator.SetBool("Hit", true);
            dodoAnimator.SetBool("isIdle", false);
            dodoAnimator.SetBool("attacking", false);
            dodoAnimator.SetBool("isDead", false);
        }
    }

    public void Idle() {
        if (birbObjectYellow.activeInHierarchy == true) {
            birbAnimator.SetBool("isIdle", true);
            birbAnimator.SetBool("Hit", false);
            birbAnimator.SetBool("attacking", false);
            birbAnimator.SetBool("isDead", false);
        }
        if (birbObjectGreen.activeInHierarchy == true) {
            birbAnimatorG.SetBool("isIdle", true);
            birbAnimatorG.SetBool("Hit", false);
            birbAnimatorG.SetBool("attacking", false);
            birbAnimatorG.SetBool("isDead", false);
        }
        if (birbObjectRed.activeInHierarchy == true) {
            birbAnimatorR.SetBool("isIdle", true);
            birbAnimatorR.SetBool("Hit", false);
            birbAnimatorR.SetBool("attacking", false);
            birbAnimatorR.SetBool("isDead", false);
        }
        if (dodoObject.activeInHierarchy == true) {
            dodoAnimator.SetBool("isIdle", true);
            dodoAnimator.SetBool("Hit", false);
            dodoAnimator.SetBool("attacking", false);
            dodoAnimator.SetBool("isDead", false);
        }
    }

    public void Attack() {
        if (birbObjectYellow.activeInHierarchy == true) {
            birbAnimator.SetBool("attacking", true);
            birbAnimator.SetBool("Hit", false);
            birbAnimator.SetBool("isIdle", false);
            birbAnimator.SetBool("isDead", false);
        }
        if (birbObjectGreen.activeInHierarchy == true) {
            birbAnimatorG.SetBool("attacking", true);
            birbAnimatorG.SetBool("Hit", false);
            birbAnimatorG.SetBool("isIdle", false);
            birbAnimatorG.SetBool("isDead", false);
        }
        if (birbObjectRed.activeInHierarchy == true) {
            birbAnimatorR.SetBool("attacking", true);
            birbAnimatorR.SetBool("Hit", false);
            birbAnimatorR.SetBool("isIdle", false);
            birbAnimatorR.SetBool("isDead", false);
        }
        if (dodoObject.activeInHierarchy == true) {
            dodoAnimator.SetBool("attacking", true);
            dodoAnimator.SetBool("Hit", false);
            dodoAnimator.SetBool("isIdle", false);
            dodoAnimator.SetBool("isDead", false);
        }
    }


    public void DieY() {
        birbAnimator.SetBool("Hit", false);
        birbAnimator.SetBool("isIdle", false);
        birbAnimator.SetBool("attacking", false);
        StartCoroutine("OnCompleteDeadAnimationYellow");
    }

    public void DieG() {
        birbAnimatorG.SetBool("Hit", false);
        birbAnimatorG.SetBool("isIdle", false);
        birbAnimatorG.SetBool("attacking", false);
        StartCoroutine("OnCompleteDeadAnimationGreen");
    }
    public void DieR() {
        birbAnimatorR.SetBool("Hit", false);
        birbAnimatorR.SetBool("isIdle", false);
        birbAnimatorR.SetBool("attacking", false);
        StartCoroutine("OnCompleteDeadAnimationRed");
    }
    public void DieD() {
        dodoAnimator.SetBool("Hit", false);
        dodoAnimator.SetBool("isIdle", false);
        dodoAnimator.SetBool("attacking", false);
        StartCoroutine("OnCompleteDeadAnimationDodo");
    }

    public void DieToD() {
        birbAnimator.SetBool("Hit", false);
        birbAnimator.SetBool("isIdle", false);
        birbAnimator.SetBool("attacking", false);

        birbAnimatorG.SetBool("Hit", false);
        birbAnimatorG.SetBool("isIdle", false);
        birbAnimatorG.SetBool("attacking", false);

        birbAnimatorR.SetBool("Hit", false);
        birbAnimatorR.SetBool("isIdle", false);
        birbAnimatorR.SetBool("attacking", false);

        StartCoroutine("OnCompleteDeadAnimationtoDodo");
    }



    //
    IEnumerator OnCompleteDeadAnimationYellow() {
        birbAnimator.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        deactivateYellow();
    }
    public void deactivateYellow() {
        birbObjectYellow.SetActive(false);
        StartCoroutine("WalkFalseY");
    }
    IEnumerator WalkFalseY() {
        birbObjectGreen.SetActive(true);
        GM.playerIsHit = false;
        GM.stop = false;
        yield return new WaitForSeconds(2f);
        birbAnimatorG.SetBool("Walk", false);
        
    }


    //
    IEnumerator OnCompleteDeadAnimationGreen() {
        birbAnimatorG.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        deactivateGreen();
    }
    public void deactivateGreen() {
        birbObjectGreen.SetActive(false);
        StartCoroutine("WalkFalseG");
    }
    IEnumerator WalkFalseG() {
        birbObjectRed.SetActive(true);
        GM.playerIsHit = false;
        GM.stop = false;
        yield return new WaitForSeconds(2f);
        birbAnimatorR.SetBool("Walk", false);
       
    }


    //
    IEnumerator OnCompleteDeadAnimationRed() {
        birbAnimatorR.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        deactivateRed();
    }
    public void deactivateRed() {
        birbObjectRed.SetActive(false);
        StartCoroutine("WalkFalseR");
    }
    IEnumerator WalkFalseR() {
        dodoObject.SetActive(true);
        GM.playerIsHit = false;
        GM.stop = false;
        yield return new WaitForSeconds(2f);
        dodoAnimator.SetBool("Walk", false);
        
    }


    //
    IEnumerator OnCompleteDeadAnimationDodo() {
        dodoAnimator.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        deactivateDodo();
    }
    public void deactivateDodo() {
        dodoObject.SetActive(false);
        StartCoroutine("WalkFalseD");
    }
    IEnumerator WalkFalseD() {
        birbObjectYellow.SetActive(true);
        GM.playerIsHit = false;
        GM.stop = false;
        yield return new WaitForSeconds(2f);
        birbAnimator.SetBool("Walk", false);
        
    }


    //
    IEnumerator OnCompleteDeadAnimationtoDodo() {

        if (dodoObject.activeInHierarchy == false) {
            birbAnimatorR.SetBool("isDead", true);
            birbAnimator.SetBool("isDead", true);
            birbAnimatorG.SetBool("isDead", true);
            yield return new WaitForSeconds(2f);
            deactivatetoDodo();
        }
    }
    public void deactivatetoDodo() {
        birbObjectRed.SetActive(false);
        birbObjectYellow.SetActive(false);
        birbObjectGreen.SetActive(false);
        StartCoroutine("WalkFalsetoD");
    }
    IEnumerator WalkFalsetoD() {
        dodoAnimator.SetBool("isDead", false);
        if (dodoObject.activeInHierarchy == false) {
            dodoObject.SetActive(true);
            dodoAnimator.SetBool("isDead", false);
            GM.playerIsHit = false;
            GM.stop = false;
            yield return new WaitForSeconds(2f);
            dodoAnimator.SetBool("Walk", false);
            
        }
    }

}



