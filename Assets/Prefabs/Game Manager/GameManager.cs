using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Cameras;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [SerializeField] string levelToUnlock;
    [SerializeField] string levelToLoad = "LevelSelect";

    // respawn
    private Vector3 respawnPos;

    // Camera
    private GameObject camRig, camPivot;

    // player activated skills
    public bool canJump = false;
    //private bool isCameraFollowing = false;
    public bool isGravityInverted = false;
    public bool is3d = false;

    // pickup array to respawn pickups
    private Pickup[] pickupsArray;


    private void Awake() {
        instance = this;
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu")) { // disable cursor unless it is main menu
            EnableCursor(false);
        }
            

        // initialize skills
        //if (PlayerPrefs.GetInt("3d_unlocked") == 1)
        //    Enable3d();

        pickupsArray = FindObjectsOfType<Pickup>();
    }

    private void Start() {
        // reference to camera
        camRig = FindObjectOfType<FreeLookCam>().gameObject;
        camPivot = camRig.transform.GetChild(0).gameObject;

        // initialize respawn position
        respawnPos = PlayerController.instance.transform.position; // set respawn position

        // play background music
        AudioManager.instance.PlayMusic(AudioManager.instance.backingTrackIndex);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !SceneManager.GetActiveScene().name.Equals("MainMenu")) {
            PauseUnpause();
        }
    }

    private void ResetParameters() {
        PlayerController.instance.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
        RespawnPickups();

        PlayerController.instance.transform.position = respawnPos; // put player at respawn position
        camRig.transform.position = respawnPos; // put camera at respawn position
        PlayerController.instance.gameObject.SetActive(true); // player enabled
    }

    private void RespawnPickups() {
        foreach (Pickup pickup in pickupsArray) {
            pickup.gameObject.SetActive(true);
        }
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
        FindObjectOfType<AudioManager>().PlayMusic(2); // play win music
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
