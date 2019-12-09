using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static bool canJump = false;
    public static bool isCameraFollowing = false;
    public static bool isGravityInverted = false;
    public static bool is3d = false;

    public static int currentSceneIndex;

    [SerializeField] float gravity = 50f;

    private void Awake() {
        // Singleton pattern
        int numGameController = FindObjectsOfType<GameController>().Length;
        if (numGameController > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
            Physics.gravity = new Vector3(0f, -gravity, 0f);
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }        
    }

    private void Update() {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    private static void resetParameters() {
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

    public static void loadNextLevel() {
        resetParameters();
        SceneManager.LoadScene(++currentSceneIndex);
    }

    public static void loadPreviousLevel() {
        resetParameters();
        SceneManager.LoadScene(--currentSceneIndex);
    }

    public static void resetScene() {
        resetParameters();
        SceneManager.LoadScene(currentSceneIndex);
    }

}
