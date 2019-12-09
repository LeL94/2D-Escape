using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 800f;

    Rigidbody rb;
    SphereCollider collider;
    bool isGrounded;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
    }

    private void FixedUpdate() {
        PlayerMovement();
    }

    private void PlayerMovement() {
        Move();
        Jump();
    }

    private void Move() {
        float h = Input.GetAxis("Horizontal"); // value is between -1 to +1
        float v = Input.GetAxis("Vertical");

        if (GameController.is3d) {
            Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

            rb.velocity = Vector3.Normalize(m_Move ) * moveSpeed + new Vector3(0f, rb.velocity.y, 0f);
        }
        else {
            rb.velocity = new Vector3(h * moveSpeed, rb.velocity.y, 0f);
        }

    }

    private void Jump() {
        if (!GameController.canJump) {
            return;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Vector3 jumpForceVector = -Physics.gravity - jumpForce * Vector3.Normalize(Physics.gravity);

            rb.AddForce(jumpForceVector);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision) {
        isGrounded = false;
    }
}
