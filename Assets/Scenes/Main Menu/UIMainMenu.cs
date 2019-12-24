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
        if (PlayerPrefs.HasKey("Continue")) {
            continueButton.interactable = true;
        }
        else {
            continueButton.interactable = false;
        }
    }

    public void NewGame() {
        PlayerPrefs.DeleteAll(); // reset all player prefs
        PlayerPrefs.SetString("CurrentLevel", firstLevel); // set first level as current level
        PlayerPrefs.SetInt(firstLevel, 1); // set first level as unlocked
        // PlayerPrefs.SetInt("Continue", 0); TODO activate when player saves
        SceneManager.LoadScene(firstLevel);
    }

    public void Continue() {
        // TODO
    }

    public void Quit () {
        Application.Quit();
    }
}
