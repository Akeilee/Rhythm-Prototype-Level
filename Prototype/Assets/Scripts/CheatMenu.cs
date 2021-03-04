using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheatMenu : MonoBehaviour{

    public GameManager GM;
    public HPSlider HP;
    public GameObject cheatMenu;
    public MusicSlider musicSlide;
    public BeatScroller bs;
    public BeatScroller bs2;
    public static bool GamePaused = false;
    public Text textChange;
    public Text timerText;
    private float maxTime = 122;

    //used to do text input
    int hPTemp;
    int hPCurrentTemp;
    int scoreTemp;
    int treasureTemp;
    float timeTemp;

    //used to reset
    int RHPTemp;
    int RScoreTemp;
    int RTreasureTemp;

    private InputField input;
    private InputField input2;
    private InputField input3;
    private InputField input4;
    private InputField input5;

    bool changed = false;

    public AudioClip openMenu;
    private AudioSource musicSource { get { return GetComponent<AudioSource>(); } }

    void Awake() {
        input = GameObject.Find("InputField").GetComponent<InputField>();
        input2 = GameObject.Find("InputField2").GetComponent<InputField>();
        input3 = GameObject.Find("InputField3").GetComponent<InputField>();
        input4 = GameObject.Find("InputField4").GetComponent<InputField>();
        input5 = GameObject.Find("InputField5").GetComponent<InputField>();
    }
    void Start(){
        ContinueGame();
        textChange.text = "";

        RHPTemp = HP.maxHealth;
        RScoreTemp = GM.currentScore;
        RTreasureTemp = GM.treasureCounter;

        musicSource.playOnAwake = false;
        perfectHits(false);
    }

    void Update(){

        //toggle cheat menu
        if (Input.GetKeyDown(KeyCode.F2) && GM.timecount <=122) {
            if (!GamePaused) {
                musicSource.PlayOneShot(openMenu);
                PauseGame();
            }
            else {
                musicSource.PlayOneShot(openMenu);
                ContinueGame();
            }
        }


        timerText.text = "Current Time: " + GM.timecount.ToString("0.##");
    }

    private void PauseGame() {
        cheatMenu.SetActive(true);
        GM.audioSource.Pause();
        Time.timeScale = 0f;
        GamePaused = true;

    }
    private void ContinueGame() {
        cheatMenu.SetActive(false);
        GM.audioSource.UnPause();
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void changeHP(string value) {

        hPTemp = (int)HP.hpbar.maxValue;
        input.text = "";

        int newHP = int.Parse(value);
        
        if (newHP >= 99999) {
            newHP = 99999;
        }
        if (newHP < 0) {
            newHP = 0;
        }

        HP.maxHealth = newHP;
        HP.hpbar.maxValue = newHP;
        HP.currentHealth = newHP;

        textChange.text = "- You changed HP from " + hPTemp + " to " + newHP + " -";
        //Debug.Log("HP Bar: " + HP.hpbar.maxValue);
        //Debug.Log("HP Max Health" + HP.maxHealth);
        //Debug.Log("Current Health" + HP.currentHealth);
    }

    public void changeCurrent(string value) {

        hPCurrentTemp = HP.currentHealth;
        input2.text = "";

        int currentHP = int.Parse(value);
        if (currentHP > HP.maxHealth && currentHP > HP.hpbar.maxValue) {
            currentHP = (int)HP.hpbar.maxValue;
        }

        HP.currentHealth = currentHP;

        if (currentHP >= 99999) {
            currentHP = 99999;
        }
        if (currentHP < 0) {
            currentHP = 0;
        }

        

        

        textChange.text = "- You changed Current HP from " + hPCurrentTemp + " to " + currentHP + " -";

        /* Debug.Log("HP Bar: " + HP.hpbar.maxValue);
           Debug.Log("HP Max Health" + HP.maxHealth);
           Debug.Log("Current Health" + HP.currentHealth);
           Debug.Log("Current Value " + HP.hpbar.value);*/
    }

    public void changeScore(string value) {

        scoreTemp = GM.currentScore;
        input3.text = "";

        int currentScore = int.Parse(value);
        

        if (currentScore >= 99999999) {
            currentScore = 99999999;
        }
        if (currentScore < 0) {
            currentScore = 0;
        }

        GM.currentScore = currentScore;

        textChange.text = "- You changed Score from " + scoreTemp + " to " + currentScore + " -";
        Debug.Log("Score " + GM.currentScore);
    }

    public void changeTreasure(string value) {
        treasureTemp = GM.treasureCounter;
        input4.text = "";

        int currentTreasure = int.Parse(value);
        GM.treasureCounter = currentTreasure;

        if (GM.treasure.activeInHierarchy == false && currentTreasure > 0) {
            GM.treasureText.text = "x" + GM.treasureCounter;
            GM.treasure.SetActive(true);
        }

        if (GM.treasure.activeInHierarchy == true && currentTreasure > 0) {
            GM.treasureText.text = "x" + GM.treasureCounter;
        }

        if (currentTreasure >= 20) {
            currentTreasure = 20;
        }
        if (currentTreasure <= 0) {
            currentTreasure = 0;
            GM.treasure.SetActive(false);
            GM.treasureText.text = "";
        }

        textChange.text = "- You changed Treasure Amount from " + treasureTemp + " to " + currentTreasure + " -";
       // Debug.Log("Treasure " + GM.treasureCounter);

    }


    public void changeTime(string value) {

        if (!changed) {
            float currentTime = float.Parse(value);
            
            timeTemp = GM.timecount;
            input5.text = "";



            if (currentTime <= timeTemp) {
                currentTime = 0;
                textChange.text = "- Invalid Time Entered -";
            }

            else {
                GM.timecount = currentTime;
                GM.audioSource.time = currentTime ;
                musicSlide.timeRemaining = currentTime;

                bs.timer = currentTime;
                bs2.timer = currentTime;
                bs.transform.position -= new Vector3(bs.beatTempo * bs.timer, 0, 0); //moving on X axis;
                bs2.transform.position -= new Vector3(bs2.beatTempo * bs2.timer, 0, 0); //moving on X axis;

                if (currentTime >= maxTime) {
                    currentTime = maxTime;
                }
                if (currentTime < 0) {
                    currentTime = 0;
                }

                textChange.text = "- You changed Time from " + timeTemp.ToString("0.##") + " to " + currentTime + " -";
            }

            changed = true;
            //Debug.Log(bs.timer);
            // Debug.Log(bs2.timer);
        }

        if (changed) {
            input5.text = "";
        }

        
    }

    public void resetValues() {
/*        HP.maxHealth = RHPTemp;
        HP.hpbar.maxValue = RHPTemp;
        HP.currentHealth = RHPTemp;
        GM.currentScore = RScoreTemp;
        GM.treasureCounter = RTreasureTemp;
        GM.timecount = 0;

        GM.treasure.SetActive(false);
        GM.treasureText.text = "";
        GM.audioSource.Stop();

        musicSlide.timeRemaining = -4;
        StartCoroutine(playMusic());

        bs.timer = 0;
        bs2.timer = 0;
        bs.transform.position = bs.pos;
        bs2.transform.position = bs2.pos;

        note.gameObject.SetActive(true);

        Debug.Log(bs.pos);*/

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator playMusic() {
        yield return new WaitForSeconds(4f);
        GM.audioSource.Play();
        GM.audioSource.time = 0;
        
    }

    public void perfectHits(bool value) {
        if (value == true) {
            NoteObject.cheat = true;
            HoldNotes.cheat = true;
        }
        else {
            NoteObject.cheat = false;
            HoldNotes.cheat = false;
        }
        
    }


    }
