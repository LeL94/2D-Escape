using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour {

    private void OnTriggerEnter(Collider collision) {
        GameManager.loadRelativeLevel(1);
    }



}
