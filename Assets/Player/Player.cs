using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float gravityForce = 3f;

    private Rigidbody rb;
    private SphereCollider myCollider;
    private float distToGround;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        myCollider = GetComponent<SphereCollider>();
        distToGround = myCollider.bounds.extents.y;
    }

    private void FixedUpdate() {
        PlayerMovement();

        // scale gravity
        rb.AddForce(Physics.gravity * gravityForce);
    }

    private void PlayerMovement() {
        Move();
        Jump();
    }

    private void Move() {
        float h = Input.GetAxisRaw("Horizontal"); // value is either -1 or +1
        float v = Input.GetAxisRaw("Vertical");

        if (GameController.getIs3dEnabled()) {
            Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

            rb.velocity = Vector3.Normalize(m_Move) * moveSpeed + new Vector3(0f, rb.velocity.y, 0f);
        }
        else {
            rb.velocity = new Vector3(h * moveSpeed, rb.velocity.y, 0f);
        }

    }

    private void Jump() {       

        if (GameController.getIsJumpEnabled() && Input.GetButtonDown("Jump")) {

            if (GameController.getIsGravityInverted()) {
                if (!isGrounded("above"))
                    return;
                rb.velocity = new Vector3(rb.velocity.x, -jumpForce, rb.velocity.z);
            }
            else {
                if (!isGrounded("below"))
                    return;
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            }            
        }
    }


    private bool isGrounded(string direction) {
        if (direction.Equals("below"))
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);

        else if (direction.Equals("above"))
            return Physics.Raycast(transform.position, Vector3.up, distToGround + 0.1f);

        else
            Debug.Log("is grounded direction note recognized");
        return false;
    }

}

/* Character controller
public class Player : MonoBehaviour {
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 150f;
    [SerializeField] float gravityScale = 3f;

    CharacterController controller;
    Rigidbody rb;

    private Vector3 moveDirection;


    private void Start() {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        float h = Input.GetAxisRaw("Horizontal"); // value is either -1 or +1
        float v = Input.GetAxisRaw("Vertical");

        if (GameController.is3d) {
            moveDirection = Vector3.Normalize(new Vector3(h, 0f, v)) * moveSpeed;
        }
        else {
            moveDirection = new Vector3(h, 0f, 0f) * moveSpeed;
        }


        if (GameController.canJump && Input.GetButtonDown("Jump")) {

            if (GameController.isGravityInverted) {
                // check if grounded
                if ((controller.collisionFlags & CollisionFlags.Above) != 0) {
                    moveDirection.y = -jumpForce;
                }
            }
            else {
                // check if grounded
                if ((controller.collisionFlags & CollisionFlags.Below) != 0) {
                    moveDirection.y = jumpForce;
                }
            }                               
        }


        // apply gravity
        moveDirection.y += Physics.gravity.y * gravityScale;


        // move
        controller.Move(moveDirection * Time.deltaTime);
        
    }


}
*/


