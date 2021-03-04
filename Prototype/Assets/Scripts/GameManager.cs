using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//for making music play
public class GameManager : MonoBehaviour {

    public AudioSource audioSource;
    //private AudioClip theMusic;

    public bool startPlaying;
    public BeatScroller theBS;
    public BeatScroller holdBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerHoldNote = 10;

    public int currentMult;
    public int multiplierTracker;
    public int[] multiplierThreshold; //mult threshold increases/ makes harder to get multiplier

    public int currentCombo;

    public Text scoreText, multiText, comboText, comboTrackText;

    public Text timer;
    public float timecount = 0;

    public const int greatScore = 125;
    public const int perfectScore = 150;
    public const int greatHoldScore = 15;
    public const int perfectHoldScore = 20;

    public HPSlider health;
    public MusicSlider musicSlide;

    public Animator playerAnimator;
    public EnemyChange enemy;

    public GameObject treasure;
    public Text treasureText;
    public int treasureCounter;

    public GameObject startPanel;

    public SoundManager soundmanage;

    private static int n;
    public bool playerIsHit = false;
    bool deadY = false;
    bool deadG = false;
    bool deadR = false;
    bool deadD = false;
    bool deadtoD = false;

    bool onetime1 = false;
    bool onetime2 = false;
    bool onetime3 = false;
    bool onetime4 = false;
    bool onetime5 = false;
    bool onetime6 = false;
    bool onetime7 = false;
    bool onetime8 = false;

    bool onceSound = false;
    bool onceSound2 = false;
    bool onceSound3 = false;

    public bool stop = false;
    bool isIdle = false;

    public int specialCounter = 0;
    public Animator fireball;
    public bool special = false;


    public float totalNotes;
    public float coolHits;
    public float greatHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHit, coolText, greatText, perfectText, missText, rankText, finalScoreText, finalTreasureText;


    void Start() {
        instance = this;
        scoreText.text = "0";

        currentMult = 1;
        multiText.text = "x" + currentMult;

        currentCombo = 0;
        comboText.text = "";
        comboTrackText.text = "";
        currentScore = 0;

        playerAnimator.SetBool("Attack", false);
        playerAnimator.SetBool("Hit", false);
        playerAnimator.SetBool("StartSongWalk", true);
        n = 0;

        treasure.SetActive(false);
        treasureText.text = "";
        treasureCounter = 0;

        //audioSource = GetComponent<AudioSource>();

        StartCoroutine(IntroSound());
        
        startPanel.SetActive(true);
        timer.gameObject.SetActive(false);
        timer.text = "";

        totalNotes = FindObjectsOfType<NoteObject>().Length + FindObjectsOfType<HoldNotes>().Length;
        resultsScreen.SetActive(false);
    }

    IEnumerator IntroSound() {
        soundmanage.PlaySound("startPanelSound");
        yield return new WaitForSeconds(1f);
        soundmanage.PlaySound("introSound");
    }

    IEnumerator PlaySound() {
        yield return new WaitForSeconds(4f);
        audioSource.Play();
    }


    void Update() {
        timecount += Time.deltaTime;
        timer.text = timecount.ToString("0");
        scoreText.text = "" + currentScore;

        if (!startPlaying) {
            //if (Input.anyKeyDown) {
                startPlaying = true;
                theBS.started = true;
                holdBS.started = true;

                StartCoroutine(PlaySound());  
            //}
        }

        //Results screen
        if (timecount >= 122 && !resultsScreen.activeInHierarchy) {
            Time.timeScale = 0f;
            resultsScreen.SetActive(true);
            coolText.text = "" + coolHits;
            greatText.text = "" + greatHits;
            perfectText.text = "" + perfectHits;
            missText.text = "" + missedHits;

            float totalHit = coolHits + greatHits + perfectHits;
            float percentage = (totalHit / totalNotes) * 100f;

            percentHit.text = percentage.ToString("F1") + "%" ; //1dp

            string rankValue = "F";

            if (percentage > 30) {
                rankValue = "E";
                if (percentage > 40) {
                    rankValue = "D";
                    if (percentage > 55) {
                        rankValue = "C";
                        if (percentage > 70) {
                            rankValue = "B";
                            if (percentage > 85) {
                                rankValue = "A";
                                if (percentage > 95) {
                                    rankValue = "S";
                                    if (percentage >= 100) {
                                        rankValue = "S+";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            rankText.text = rankValue;
            finalScoreText.text = currentScore.ToString();
            finalTreasureText.text = "x" + treasureCounter.ToString();

        }

        //toggle timer
        if (Input.GetKeyDown(KeyCode.F3)) {
            bool toggleKey = timer.gameObject.activeSelf;
            toggleKey = !toggleKey;
            timer.gameObject.SetActive(toggleKey);
        }


        ////special move
        if (specialCounter >= 20) {
            special = true;
        }

        if (timecount >= 100 && special == true) {
            playerAnimator.SetBool("Hit", false);
            playerAnimator.SetBool("Attack", false);
            playerAnimator.SetBool("Special", true);

            StartCoroutine(FireballAni());

            if (!onceSound3) {
                PlaySpecial();
            }

            enemy.dodoAnimator.SetBool("isIdle", true);
            enemy.birbAnimator.SetBool("isDead", false);
            enemy.slashPAnimator.SetBool("SlashP", false);
            enemy.slashEAnimator.SetBool("SlashB", false);
            enemy.slashDAnimator.SetBool("SlashD", false);

        }

        if (timecount >= 102 && special == true) {
            playerAnimator.SetBool("Special", false);
            fireball.SetBool("Fireball", false);
            fireball.gameObject.SetActive(false);
            special = false;
        }

        if (specialCounter < 20 || timecount < 98) {
            special = false;
        }


        //intro walk
        if (timecount < 3) {
            playerAnimator.SetBool("StartSongWalk", true);
        }
        if (timecount >= 3) {
            playerAnimator.SetBool("StartSongWalk", false);
            startPanel.SetActive(false);
        }


        if (isIdle == true) {
            enemy.Idle();
           // isIdle = false;
        }



        //Enemy Deaths
        if (!onetime1) {
            ChangeEnemies1();
        }
        if (!onetime2) {
            ChangeEnemies2();
        }
        if (!onetime3) {
            ChangeEnemies3();
        }
        if (!onetime4) {
            ChangeEnemies4();
        }
        if (!onetime5) {
            ChangeEnemies5();
        }
        if (!onetime6) {
            ChangeEnemies6();
        }
        if (!onetime7) {
            ChangeEnemies7();
        }
        if (!onetime8) {
            ChangeEnemies8();
        }

        //player dying
        if (health.currentHealth <= 0) {
            playerAnimator.SetBool("Dead", true);

            if (!onceSound2) {
                PlayDeathSound();
            }

            enemy.Idle();
            enemy.slashPAnimator.SetBool("SlashP", false);
            enemy.slashEAnimator.SetBool("SlashB", false);
            enemy.slashDAnimator.SetBool("SlashD", false);
            playerIsHit = false;
        }



        else { //player has health

            if (health.currentHealth >= 0 && timecount >=120 && playerAnimator.GetBool("Dead") != true) { //game finished

                if (!onceSound) {
                    PlayOutro();
                }
                deadD = false;
                playerAnimator.SetBool("SongFinished", true);
                playerAnimator.SetBool("Attack", false);
                playerAnimator.SetBool("Dead", false);
                playerAnimator.SetBool("Hit", false);

                playerIsHit = false;
                enemy.slashPAnimator.SetBool("SlashP", false);
                enemy.slashEAnimator.SetBool("SlashB", false);
                enemy.slashDAnimator.SetBool("SlashD", false);

                if (enemy.birbObjectYellow.activeInHierarchy == true) {
                    enemy.birbObjectYellow.SetActive(true);
                    enemy.birbAnimator.SetBool("isDead", true);
                    enemy.birbAnimator.SetBool("isIdle", false);
                    enemy.birbAnimator.SetBool("Hit", false);
                    enemy.birbAnimator.SetBool("attacking", false);
                }

                else if (enemy.dodoObject.activeInHierarchy == true) {
                    enemy.dodoAnimator.SetBool("Hit", false);
                    enemy.dodoAnimator.SetBool("isIdle", false);
                    enemy.dodoAnimator.SetBool("attacking", false);
                    enemy.dodoAnimator.SetBool("isDead", true);
                }
                
  
            }


           else if (health.currentHealth >= 0 && timecount < 122 && special == false) { //game not finished
                if (n % 2 != 0) { //if odd hits
                    playerAnimator.SetBool("Attack", true); //player attacks and enemy gets hit
                    enemy.GetHit();

                    //if enemy not active, no slash
                    if (enemy.birbObjectYellow.activeInHierarchy == false && enemy.birbObjectRed.activeInHierarchy == false &&
                        enemy.birbObjectGreen.activeInHierarchy == false && enemy.dodoObject.activeInHierarchy == false || stop == true) {
                        enemy.slashDAnimator.SetBool("SlashD", false);
                        enemy.slashEAnimator.SetBool("SlashB", false);
                    }

                    else if (enemy.birbObjectYellow.activeInHierarchy == true ||
                        enemy.birbObjectRed.activeInHierarchy == true ||
                        enemy.birbObjectGreen.activeInHierarchy == true) {
                            enemy.slashEAnimator.SetBool("SlashB", true);
                            enemy.slashDAnimator.SetBool("SlashD", false);
                    }
                    else if (enemy.dodoObject.activeInHierarchy == true) {
                                enemy.slashDAnimator.SetBool("SlashD", true);
                                enemy.slashEAnimator.SetBool("SlashB", false);
                    }

                    enemy.slashPAnimator.SetBool("SlashP", false); //player does not get slashed

                }
                else if (n % 2 == 0) {
                    playerAnimator.SetBool("Attack", false); //player does not attack

                    enemy.slashDAnimator.SetBool("SlashD", false);
                    enemy.slashEAnimator.SetBool("SlashB", false);
                    enemy.slashPAnimator.SetBool("SlashP", false);

                    if (playerAnimator.GetBool("Attack") == false && enemy.startN != 1) {
                        isIdle = true;
                    }
                    isIdle = false;
                }

                playerAnimator.SetBool("Dead", false);
            }


            if (playerIsHit == true && special == false) { //if player gets hit, enemy attacks

                //if enemy not active, no slashes
                if (enemy.birbObjectYellow.activeInHierarchy == false && enemy.birbObjectRed.activeInHierarchy == false &&
                    enemy.birbObjectGreen.activeInHierarchy == false && enemy.dodoObject.activeInHierarchy == false || stop == true) {
                    enemy.slashPAnimator.SetBool("SlashP", false);
                    playerIsHit = false;
                }

                else {
                    enemy.Attack();
                    playerAnimator.SetBool("Hit", true);
       
                    enemy.slashPAnimator.SetBool("SlashP", true);
                    enemy.slashEAnimator.SetBool("SlashB", false);
                    enemy.slashDAnimator.SetBool("SlashD", false);
                }
                
            }

            if (playerIsHit == false) {
                enemy.slashPAnimator.SetBool("SlashP", false);
            }


            if (deadY == true) {
                enemy.DieY();
                playerIsHit = false;
            }

            if (deadG == true) {
                enemy.DieG();
                playerIsHit = false;
            }

            if (deadR == true) {
                enemy.DieR();
                playerIsHit = false;
            }

            if (deadD == true) {
                enemy.DieD();
                playerIsHit = false;
            }

            if (deadtoD == true) {
                enemy.DieToD();
                playerIsHit = false;
            }

        }

    }


    IEnumerator FireballAni() {
        yield return new WaitForSeconds(1f);
        fireball.SetBool("Fireball", true);
    }
    private void PlayOutro() {
        soundmanage.PlaySound("victorySound");
        onceSound = true;
    }    
    private void PlayDeathSound() {
        soundmanage.PlaySound("playerDeathSound");
        onceSound2 = true;
    }

    private void PlaySpecial() {
        soundmanage.PlaySound("specialSound");
        onceSound3 = true;
    }

    public void ChangeEnemies1() {
        if (currentScore >= 5000 && timecount <86) {
            deadY = true;
            //if (enemy.birbObjectGreen.activeInHierarchy == true) {
            //    deadY = false;
            // }
            onetime1 = true;
            stop = true;

            treasure.SetActive(true);
            treasureCounter++;
            treasureText.text = "x" + treasureCounter;
        }
    }
    public void ChangeEnemies2() {
        if (currentScore >= 15000 && timecount < 86) {
            deadG = true;
            onetime2 = true;
            deadY = false;
            stop = true;

            if (treasure.activeInHierarchy == false) {
                treasure.SetActive(true);
            }
            treasureCounter++;
            treasureText.text = "x" + treasureCounter;
        }
    }
    public void ChangeEnemies3() {
        if (currentScore >= 30000 && timecount < 86) {
            deadR = true;
            onetime3 = true;
            deadG = false;
            stop = true;

            if (treasure.activeInHierarchy == false) {
                treasure.SetActive(true);
            }
            treasureCounter++;
            treasureText.text = "x" + treasureCounter;
        }
        
    }
    public void ChangeEnemies4() {
        if (currentScore >= 45000 && timecount < 86) {
            deadD = true;
            onetime4 = true;
            deadR = false;
            stop = true;

            if (treasure.activeInHierarchy == false) {
                treasure.SetActive(true);
            }
            treasureCounter++;
            treasureText.text = "x" + treasureCounter;
        }
        
    }
    public void ChangeEnemies5() {
        if (currentScore >= 60000 && timecount < 86) {
            deadY = true;
            onetime5 = true;
            deadD = false;
            stop = true;

            if (treasure.activeInHierarchy == false) {
                treasure.SetActive(true);
            }
            treasureCounter++;
            treasureText.text = "x" + treasureCounter;
        }
       
    }
    public void ChangeEnemies6() {
        if (currentScore >= 75000 && timecount < 86) {
            deadG = true;
            onetime6 = true;
            deadY = false;
            stop = true;

            if (treasure.activeInHierarchy == false) {
                treasure.SetActive(true);
            }
            treasureCounter++;
            treasureText.text = "x" + treasureCounter;
        }
        
    }
    public void ChangeEnemies7() {
        if ((timecount >= 86)) {
            deadtoD = true;
            onetime7 = true;
            deadG = false;
            deadD = false;
            deadR = false;
            deadY = false;
            stop = true;

            if (treasure.activeInHierarchy == false) {
                treasure.SetActive(true);
            }
            treasureCounter++;
            treasureText.text = "x" + treasureCounter;
        }
        
    }
    public void ChangeEnemies8() {
        if ((timecount >=102 && specialCounter >=20)) {
            deadD = true;
            onetime8 = true;
            deadtoD = false;
            stop = true;

            if (treasure.activeInHierarchy == false) {
                treasure.SetActive(true);
            }
            
            treasureCounter++;
            treasureText.text = "x" + treasureCounter;
        }
        
    }


    public void NoteHit() {
        /////
        if (timecount >= 87 && timecount <= 100) {
            specialCounter++;
        }

        //Debug.Log("Hit");

        if (currentMult - 1 < multiplierThreshold.Length) //bounds to length of arary
        {
            multiplierTracker++;

            if (multiplierThreshold[currentMult - 1] <= multiplierTracker) {
                multiplierTracker = 0;
                currentMult++;
            }
        }

        currentCombo++;

        multiText.text = "x" + currentMult;
        comboTrackText.text = "" + currentCombo;
        comboText.text = "combo";

        playerAnimator.SetBool("Hit", false);

        playerIsHit = false;
    }
    public void CoolHit() {
        currentScore += scorePerNote * currentMult;
        n++;
        NoteHit();

        coolHits++;
    }
    public void GreatHit() {
        currentScore += greatScore * currentMult;
        n++;
        NoteHit();

        greatHits++;
    }
    public void PerfectHit() {
        currentScore += perfectScore * currentMult;
        n++;
        NoteHit();

        perfectHits++;
    }

    public void NoteMissed() {
        //Debug.Log("Miss");

        currentMult = 1; //reset multiplier
        multiplierTracker = 0;
        multiText.text = "x" + currentMult;

        currentCombo = 0;
        comboText.text = "";
        comboTrackText.text = "";

        health.TakeDamage(10);

        if (health.currentHealth <= 0 && special == false) {
            playerAnimator.SetBool("Hit", false);
            playerAnimator.SetBool("Dead", true);
            enemy.Idle();
        }

        else if ((health.currentHealth >= 0 && special == false) ||(health.currentHealth >= 0 && timecount>=101)) {
            if (stop == true && special == false) {
                playerAnimator.SetBool("Hit", false);
            }

           // playerAnimator.SetBool("Hit", true);
            playerAnimator.SetBool("Idle", false);
            playerAnimator.SetBool("Dead", false);
            playerIsHit = true;
            
        }

       /* else
            playerAnimator.SetBool("SongFinished", true);*/

        n = 0; //reset n
        missedHits++;
    }


    public void HoldNoteMissed() {
        //Debug.Log("Miss");

        currentMult = 1; //reset multiplier
        multiplierTracker = 0;
        multiText.text = "x" + currentMult;

        currentCombo = 0;
        comboText.text = "";
        comboTrackText.text = "";

        health.TakeDamage(2);

        if (health.currentHealth <= 0 && special == false) {
            playerAnimator.SetBool("Hit", false);
            playerAnimator.SetBool("Dead", true);
        }

        else if (health.currentHealth >= 0 && special == false) {

            if (stop == true && special == false) {
                playerAnimator.SetBool("Hit", false);
            }

            //playerAnimator.SetBool("Hit", true);
            playerAnimator.SetBool("Dead", false);
            playerIsHit = true;

        }

        n = 0;
        missedHits++;
    }

    public void HoldNoteHit() {

        currentCombo++;

        
        multiText.text = "x" + currentMult;
        comboTrackText.text = "" + currentCombo;
        comboText.text = "combo";

        playerAnimator.SetBool("Hit", false);

        playerIsHit = false;

        soundmanage.PlaySound("pressSound");

    }
    public void HoldCoolHit() {
        currentScore += scorePerHoldNote * currentMult;
        n++;
        HoldNoteHit();

        coolHits++;
    }
    public void HoldGreatHit() {
        currentScore += greatHoldScore * currentMult;
        n++;
        HoldNoteHit();

        greatHits++;
    }
    public void HoldPerfectHit() {
        currentScore += perfectHoldScore * currentMult;
        n++;
        HoldNoteHit();

        perfectHits++;
    }



}
