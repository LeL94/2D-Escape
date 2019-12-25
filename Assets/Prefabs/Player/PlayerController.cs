using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 17f;
    [SerializeField] float gravityForce = 3f;

    private Rigidbody rb;
    private SphereCollider myCollider;
    private float distToGround;

    // improve jumping: when press jump before being grounded
    private float jumpPressedRemember = 0f;
    private float jumpPressedRememberTime = 0.2f;

    // improve jumping: when press jump after being grounded
    private float groundedRembember = 0f;
    private float groundedRembemberTime = 0.2f;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        myCollider = GetComponent<SphereCollider>();
        distToGround = myCollider.bounds.extents.y;
    }

    private void Update() {
        // enable 3d
        if (PlayerPrefs.GetInt("3d_unlocked") == 1 && Input.GetKeyDown(Config.Key3d))
            GameManager.instance.Switch3dView();
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

        if (GameManager.instance.is3d) {
            Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

            rb.velocity = Vector3.Normalize(m_Move) * moveSpeed + new Vector3(0f, rb.velocity.y, 0f);
        }
        else {
            rb.velocity = new Vector3(h * moveSpeed, rb.velocity.y, 0f);
        }

    }

    private void Jump() {
        if (!GameManager.instance.canJump)
            return;


        // jumping even if jump button was pressed 0.2s ago
        if (jumpPressedRemember > 0)
            jumpPressedRemember -= Time.deltaTime;

        // jumping even if was grounded 0.2s ago
        if (groundedRembember > 0)
            groundedRembember -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
            jumpPressedRemember = jumpPressedRememberTime;

        // gravity normal
        if (!GameManager.instance.isGravityInverted) {
            if (isGrounded("below"))
                groundedRembember = groundedRembemberTime;

            if (groundedRembember > 0 && jumpPressedRemember > 0) {
                jumpPressedRemember = 0f;
                groundedRembember = 0f;
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                FindObjectOfType<AudioManager>().PlaySFX(3); // play jump SFX
            }
        }
        // gravity inverted
        else {
            if (isGrounded("above"))
                groundedRembember = groundedRembemberTime;

            if (groundedRembember > 0 && jumpPressedRemember > 0) {
                jumpPressedRemember = 0f;
                rb.velocity = new Vector3(rb.velocity.x, -jumpForce, rb.velocity.z);
                FindObjectOfType<AudioManager>().PlaySFX(3); // play jump SFX
            }
        }
    }


    private bool isGrounded(string direction) {
        if (direction.Equals("below"))
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);

        else if (direction.Equals("above"))
            return Physics.Raycast(transform.position, Vector3.up, distToGround + 0.1f);

        else
            Debug.Log("Player: is grounded direction not recognized");
        return false;
    }
}


