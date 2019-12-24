using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [SerializeField] string levelToUnlock;
    [SerializeField] string levelToLoad = "LevelSelect";
    [SerializeField] float timeToEndLevel = 0f;

    private bool canJump = false;
    private bool isCameraFollowing = false;
    private bool isGravityInverted = false;
    private bool is3d = false;

    private int currentSceneIndex;


    private void Awake() {
        instance = this;
        EnableCursor(false);
        Debug.Log("cursor disabled");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseUnpause();
        }
    }

    private void ResetParameters() {
        canJump = false;
        isCameraFollowing = false;

        if (isGravityInverted) {
            isGravityInverted = false;
            Physics.gravity *= -1;
        }

        if (is3d) {
            is3d = false;
            Camera.main.orthographic = true;
        }
    }

    public void loadRelativeLevel(int level) {
        ResetParameters();
        // level = -1 : previous level
        // level = 0: current level
        // level = 1: next level
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex = currentSceneIndex + level;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // pick ups
    public void enableJump(bool setCanJump) {
        canJump = setCanJump;
    }

    public void enable3d() {
        is3d = true;
        Camera.main.orthographic = false;
    }

    public void invertGravity() {
        isGravityInverted = !isGravityInverted;
        Physics.gravity *= -1;
        //GameObject player = GameObject.FindWithTag("Player");
        //player.transform.Rotate(180f, 0f, 0f);
    }


    // getters
    public bool getIsJumpEnabled() {
        return canJump;
    }

    public bool getIsGravityInverted() {
        return isGravityInverted;
    }

    public bool getIs3dEnabled() {
        return is3d;
    }
    
    public int getCurrentLevel() {
        return currentSceneIndex;
    }

    //
    public void PauseUnpause() {
        if (UIManager.instance.pauseScreen.activeInHierarchy) {
            UIManager.instance.pauseScreen.SetActive(false); // close pause menu
            Time.timeScale = 1; // normal time
            EnableCursor(false); // hide and lock cursor
        }
        else {
            UIManager.instance.pauseScreen.SetActive(true); // open pause menu
            Time.timeScale = 0f; // freeze time
            EnableCursor(true); // free cursor
            UIManager.instance.CloseOptions(); // close option screen in case it is open
        }
    }

    private void EnableCursor(bool cursorEnabled) {
        if (cursorEnabled) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void EndLevel() {
        StartCoroutine(EndLevelCo());
    }

    private IEnumerator EndLevelCo() {
        yield return new WaitForSeconds(timeToEndLevel);
        ResetParameters();

        // unlock level
        PlayerPrefs.SetInt(levelToUnlock, 1);

        // load next level
        SceneManager.LoadScene(levelToLoad);
    }
}
