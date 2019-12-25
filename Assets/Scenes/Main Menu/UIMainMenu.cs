using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private string firstLevel = "Level 0";

    public Button continueButton;

    private void Awake() {

        // activate or deactivate continue button
        if (PlayerPrefs.HasKey("Continue")) {
            continueButton.interactable = true;
        }
        else {
            continueButton.interactable = false;
        }
    }

    public void NewGame() {
        FindObjectOfType<AudioManager>().PlaySFX(0); // play menu click SFX
        InitializePlayerPrefs();        
        SceneManager.LoadScene(firstLevel);
    }

    public void Continue() {
        FindObjectOfType<AudioManager>().PlaySFX(0); // play menu click SFX
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit () {
        FindObjectOfType<AudioManager>().PlaySFX(0); // play menu click SFX
        Application.Quit();
    }

    public void InitializePlayerPrefs() {
        PlayerPrefs.DeleteAll(); // reset all player prefs
        PlayerPrefs.SetString("CurrentLevel", firstLevel); // set first level as current level
        PlayerPrefs.SetInt(firstLevel, 1); // set first level as unlocked

        // skills
        PlayerPrefs.SetInt("jump_unlocked", 0);
        PlayerPrefs.SetInt("double_jump_unlocked", 0);
        PlayerPrefs.SetInt("3d_unlocked", 0);


        // PlayerPrefs.SetInt("Continue", 0); TODO activate when player saves
    }
}
