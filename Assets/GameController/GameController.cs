using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    private static bool canJump = false;
    private static bool isCameraFollowing = false;
    private static bool isGravityInverted = false;
    private static bool is3d = false;

    private static int currentSceneIndex;


    private void Awake() {
        // disable bound rendering
        /*GameObject[] bounds = GameObject.FindGameObjectsWithTag("EditorOnly");
        foreach (GameObject bound in bounds) {
            MeshRenderer mesh = bound.GetComponent<MeshRenderer>();
            mesh.enabled = false;
        }*/

        // Singleton pattern
        int numGameController = FindObjectsOfType<GameController>().Length;
        if (numGameController > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
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

    public static void loadRelativeLevel(int level) {
        resetParameters();
        // level = -1 : previous level
        // level = 0: current level
        // level = 1: next level
        currentSceneIndex = currentSceneIndex + level;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // pick ups
    public static void enableJump(bool setCanJump) {
        canJump = setCanJump;
    }

    public static void enable3d() {
        is3d = true;
        Camera.main.orthographic = false;
    }

    public static void invertGravity() {
        isGravityInverted = !isGravityInverted;
        Physics.gravity *= -1;
        //GameObject player = GameObject.FindWithTag("Player");
        //player.transform.Rotate(180f, 0f, 0f);
    }


    // getters
    public static bool getIsJumpEnabled() {
        return canJump;
    }

    public static bool getIsGravityInverted() {
        return isGravityInverted;
    }

    public static bool getIs3dEnabled() {
        return is3d;
    }
    
    public static int getCurrentLevel() {
        return currentSceneIndex;
    }
}
