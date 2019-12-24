/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    [SerializeField] float turnSpeed = 4f;
    [SerializeField] float alpha_min = 10f;
    [SerializeField] float alpha_max = 60f;


    private GameObject target;
    private Vector3 offset;
    GameController gc;

    private void Start() {
        GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

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
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) *
                Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.left) *
                offset;
            transform.position = target.transform.position + offset;

            // constrain vertical position to limit x angle:
            float dh = transform.position.y - target.transform.position.y;
            
            if (dh < 0)
                transform.position = new Vector3(transform.position.x,
                    target.transform.position.y,
                    transform.position.z);
            
            transform.LookAt(target.transform.position);
        }
    }
}
*/