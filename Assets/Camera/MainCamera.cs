using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    [SerializeField] float turnSpeed = 4f;

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

    void LateUpdate() {
        if (GameController.isCameraFollowing && GameController.is3d) {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            transform.position = target.transform.position + offset;
            transform.LookAt(target.transform.position);
        }
    }
}
