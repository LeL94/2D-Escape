using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    [SerializeField] private string levelName;
    public GameObject mpOff, mpOn, particles;

    private bool canLoadLevel = false;
    private bool isThisLevelUnlocked = false;

    private void Start() {

        // check if this level is unlocked (it is unlocked in GameManager -> LevelEndCo())
        isThisLevelUnlocked = PlayerPrefs.GetInt(levelName) == 1;

        // set correct map point
        mpOff.SetActive(!isThisLevelUnlocked);
        mpOn.SetActive(isThisLevelUnlocked);

        // if player just finished this level, put him here
        if (PlayerPrefs.GetString("CurrentLevel").Equals(levelName)) {
            //Debug.Log("Current level is: " + levelName + ". Moving player at pos: " + transform.position);
            PlayerController.instance.transform.position = transform.position;
        }

    }

    private void OnTriggerEnter(Collider other) {
        DisplayLevelInfo(true);

        if (other.CompareTag("Player") && isThisLevelUnlocked) {
            SetLevelReadyToLoad(true);            
        }
    }

    private void OnTriggerExit(Collider other) {
        DisplayLevelInfo(false);

        if (other.CompareTag("Player") && isThisLevelUnlocked) {
            SetLevelReadyToLoad(false);            
        }
    }

    private void Update() {

        // if level selected and press jump, load level
        if (Input.GetButtonDown("Jump") && canLoadLevel) {
            PlayerPrefs.SetString("CurrentLevel", levelName); // set this level as current level
            //Debug.Log(levelName + " has been set as current level.");
            SceneManager.LoadScene(levelName); // load level
        }
    }

    private void SetLevelReadyToLoad(bool isReadyToLoad) {
        canLoadLevel = isReadyToLoad;
        particles.SetActive(isReadyToLoad);       
    }

    private void DisplayLevelInfo(bool displayLevelInfo) {
        if (displayLevelInfo) {

            if (isThisLevelUnlocked) { // if level unlocked, display level name
                LSUIManager.instance.levelInfoPanel.SetActive(true);
                LSUIManager.instance.levelLockedPanel.SetActive(false);
                LSUIManager.instance.levelSelectedText.text = levelName;
            } else { // else display locked text
                LSUIManager.instance.levelInfoPanel.SetActive(false);
                LSUIManager.instance.levelLockedPanel.SetActive(true);
            }

        }
        else {
            LSUIManager.instance.levelInfoPanel.SetActive(false);
            LSUIManager.instance.levelLockedPanel.SetActive(false);
        }


        
    }
}
