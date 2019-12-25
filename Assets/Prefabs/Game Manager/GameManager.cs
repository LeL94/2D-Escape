using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Cameras;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [SerializeField] string levelToUnlock;
    [SerializeField] string levelToLoad = "LevelSelect";

    // Camera
    private GameObject camRig, camPivot;


    public bool canJump = false;
    //private bool isCameraFollowing = false;
    public bool isGravityInverted = false;
    public bool is3d = false;


    private void Awake() {
        instance = this;
        EnableCursor(false);

        // initialize skills
        //if (PlayerPrefs.GetInt("3d_unlocked") == 1)
        //    Enable3d();
    }

    private void Start() {
        camRig = FindObjectOfType<FreeLookCam>().gameObject;
        camPivot = camRig.transform.GetChild(0).gameObject;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseUnpause();
        }
    }

    private void ResetParameters() {
        canJump = false;
        //isCameraFollowing = false;

        if (isGravityInverted) {
            isGravityInverted = false;
            Physics.gravity *= -1;
        }

        if (is3d) {
            is3d = false;
            Camera.main.orthographic = true;
        }
    }

    public void Respawn() {
        
        FindObjectOfType<AudioManager>().PlaySFX(1); // play death SFX
        PlayerController.instance.gameObject.SetActive(false); // player inactive
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo() {
        
        yield return new WaitForSeconds(Config.timeToRespawn);

        ResetParameters();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // pick ups
    public void EnableJump(bool setCanJump) {
        canJump = setCanJump;
    }

    public void Switch3dView() {
        if (!is3d) {
            is3d = true;
            Camera.main.orthographic = false;
            // reset camera rotation
            //camRig.GetComponent<FreeLookCam>().enabled = false;
            //camRig.transform.rotation = Quaternion.Euler(Vector3.zero);
            //camPivot.transform.rotation = Quaternion.Euler(Vector3.zero);
            //camRig.GetComponent<FreeLookCam>().enabled = true;
        }
        else {
            is3d = false;
            Camera.main.orthographic = true;
            // reset camera rotation
            camRig.transform.rotation = Quaternion.Euler(Vector3.zero);
            camPivot.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        
    }

    public void InvertGravity() {
        isGravityInverted = !isGravityInverted;
        Physics.gravity *= -1;
    }

    //
    public void PauseUnpause() {
        if (UIManager.instance.pauseScreen.activeInHierarchy) {
            FindObjectOfType<AudioManager>().PlaySFX(0); // play menu click SFX (no need for the other case because already called by CloseOptions())
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
        FindObjectOfType<AudioManager>().PlaySFX(2); // play win SFX
        PlayerController.instance.gameObject.SetActive(false); // player inactive
        StartCoroutine(EndLevelCo());
    }

    private IEnumerator EndLevelCo() {
        yield return new WaitForSeconds(Config.timeToEndLevel);
        ResetParameters();

        // unlock level
        PlayerPrefs.SetInt(levelToUnlock, 1);

        // load next level
        SceneManager.LoadScene(levelToLoad);

        // enable continue button
        PlayerPrefs.SetInt("Continue", 0);
    }
}
