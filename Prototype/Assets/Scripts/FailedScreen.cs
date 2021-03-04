using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedScreen : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject failScreen;
    public GameManager GM;
    public HPSlider health;
    public GameObject hideBackground;

    public AudioClip gameOverSound;
    private AudioSource musicSource { get { return GetComponent<AudioSource>(); } }

    float timer;
    bool restart = false;
    void Start() {
        ContinueGame();
        hideBackground.SetActive(false);
        gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
    }
    void Update() {
        if (health.currentHealth <= 0) {
            timer += Time.deltaTime;
            PauseGame();
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

        if (timer >= 1.5) {
            failScreen.SetActive(true);
            GM.audioSource.Pause();
            Time.timeScale = 0f;
            musicSource.PlayOneShot(gameOverSound);
            GamePaused = true;
            timer = 0;
        }

    }
    private void ContinueGame() {
        failScreen.SetActive(false);
        GM.audioSource.UnPause();
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void MainMenu() {
        Debug.Log("Back to Main Menu");
        // Time.timeScale = 1f;
        // SceneManager.LoadScene("Menu");

    }

    public void RestartGame() {
        restart = true;
        StartCoroutine(delayedStart());
    }
    IEnumerator delayedStart() {
        yield return new WaitForSeconds(0.5f);
        //Debug.Log("Restart Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        failScreen.SetActive(false);
        hideBackground.SetActive(false);
        restart = false;
        GamePaused = false;
    }
}
