using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Black Screen")]
    public Image blackScreen;
    public bool fadeToBlack, fadeFromBlack;

    // Health
    [Header("Health")]
    public Text healthText;
    public Image healthImage;
    public Sprite[] healthSprites;

    // Score
    [Header("Score")]
    public Text scoreText;

    // Pause
    [Header("Pause Screen")]
    public GameObject pauseScreen, optionsScreen;
    public Button levelSelectButton;

    // volume sliders
    public Slider musicVolumeSlider, sfxVolumeSlider;


    [SerializeField] private float fadeSpeed = 2f;

    private void Awake() {
        instance = this;

        // if this scene is level select, deactivate level select button
        if (SceneManager.GetActiveScene().name.Equals("LevelSelect")) {
            levelSelectButton.interactable = false;
        }
        else {
            levelSelectButton.interactable = true;
        }
    }

    private void Update() {
        if (fadeToBlack) {
            blackScreen.color = new Color(
            blackScreen.color.r,
            blackScreen.color.g,
            blackScreen.color.b,
            Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
                fadeToBlack = false;
        }

        if (fadeFromBlack) {
            blackScreen.color = new Color(
            blackScreen.color.r,
            blackScreen.color.g,
            blackScreen.color.b,
            Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)
                fadeFromBlack = false;
        }
    }

    public void UpdateHealth(int currentHealth) {
        healthText.text = currentHealth.ToString();

        healthImage.enabled = true;
        switch (currentHealth) {
            case 5:
                healthImage.sprite = healthSprites[4];
                break;
            case 4:
                healthImage.sprite = healthSprites[3];
                break;
            case 3:
                healthImage.sprite = healthSprites[2];
                break;
            case 2:
                healthImage.sprite = healthSprites[1];
                break;
            case 1:
                healthImage.sprite = healthSprites[0];
                break;
            case 0:
                healthImage.enabled = false;
                break;
        }
    }

    public void UpdateScore(int currentScore) {
        scoreText.text = currentScore.ToString();
    }


    // Pause Screen
    public void ResumeGame() {
        FindObjectOfType<AudioManager>().PlaySFX(0); // play menu click SFX
        GameManager.instance.PauseUnpause();
    }

    public void OpenOptions() {
        FindObjectOfType<AudioManager>().PlaySFX(0); // play menu click SFX
        optionsScreen.SetActive(true);
    }

    public void CloseOptions() {
        FindObjectOfType<AudioManager>().PlaySFX(0); // play menu click SFX
        optionsScreen.SetActive(false);
    }

    public void SelectLevel() {
        FindObjectOfType<AudioManager>().PlaySFX(0); // play menu click SFX
        Time.timeScale = 1f; // set normal time
        SceneManager.LoadScene("LevelSelect");
    }

    public void MainMenu() {
        FindObjectOfType<AudioManager>().PlaySFX(0); // play menu click SFX
        Time.timeScale = 1f; // set normal time
        SceneManager.LoadScene("MainMenu");
    }

    // volume options
    public void SetMusicVolume() {
        //AudioManager.instance.SetMusicVolume();
    }
    
    public void SetSFXVolume() {
        //AudioManager.instance.SetSFXVolume();
    }
}
