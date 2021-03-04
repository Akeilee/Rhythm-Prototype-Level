using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//pause menu
public class Pause : MonoBehaviour {

    public static bool GamePaused = false;
    public GameObject pauseUI;
    public GameManager GM;
    public HPSlider health;
    public GameObject hideBackground;

    public AudioClip openMenu;
    private AudioSource musicSource { get { return GetComponent<AudioSource>(); } }

    bool restart = false;
    void Start() {
        ContinueGame();
        hideBackground.SetActive(false);

        gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && health.currentHealth > 0 && GM.timecount <=122) {
            if (!GamePaused) {
                musicSource.PlayOneShot(openMenu);
                PauseGame();

                
            }
            else {
                musicSource.PlayOneShot(openMenu);
                ContinueGame();
                restart = false;
            }
        }

        if (restart == true) {
            Time.timeScale = 1f;
            hideBackground.SetActive(true);
            GamePaused = false;
        }

        if (restart == false) {
            hideBackground.SetActive(false);
        }
        
    }
    private void PauseGame() {
        pauseUI.SetActive(true);
        //AudioListener.pause = true;
        GM.audioSource.Pause();
        Time.timeScale = 0f;
        GamePaused = true;

    }
    private void ContinueGame() {
        pauseUI.SetActive(false);
        GM.audioSource.UnPause();
        //AudioListener.pause = false;
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void MainMenu(){
        Debug.Log("Back to Main Menu");
       // Time.timeScale = 1f;
       // SceneManager.LoadScene("Menu");

    }
    public void QuitGame() {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void RestartGame() {
        restart = true;
        StartCoroutine(delayedStart());
    }
    IEnumerator delayedStart() {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("Restart Game");
        pauseUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        hideBackground.SetActive(false);
        restart = false;
        GamePaused = false;
    }

}
