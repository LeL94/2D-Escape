using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    private GameObject target;
    private Vector3 offset;

    public void StartFollowing() {
        target = GameObject.FindWithTag("Player");
        offset = transform.position - target.transform.position;
        GameController.isCameraFollowing = true;
    }

    private void Update() {
        if (GameController.isCameraFollowing) {
            transform.position = target.transform.position + offset;
        }
    }
}
