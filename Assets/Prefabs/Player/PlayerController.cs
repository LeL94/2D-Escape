using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    [Header("Audio")]
    [SerializeField] private int jumpSfxIndex = 2;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private float gravityForce = 3f;    

    private Rigidbody rb;
    private SphereCollider myCollider;
    private float distToGround;

    // double jump
    private const int MAX_CONSECUTIVE_JUMPS = 2;
    private int currentJump;

    // improve jumping: when press jump before being grounded
    private float jumpPressedRemember = 0f;
    private const float JUMP_PRESSED_REMBEMBER_TIME = 0.2f;

    // improve jumping: when press jump after being grounded
    private float groundedRembember = 0f;
    private const float GROUNDED_REMEMBER_TIME = 0.1f;

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
        if (PlayerPrefs.GetInt("3d_unlocked") == 1 && Input.GetButtonDown("Fire1"))
            GameManager.instance.Switch3dView();

        // reset jump for double jump
        if (IsGrounded())
            currentJump = 0;
    }

    private void FixedUpdate() {
        PlayerMovement();

        // scale gravity
        rb.AddForce(Physics.gravity * gravityForce);
    }

    private void PlayerMovement() {
        Move();
        if (GameManager.instance.canJump)
            Jump();
    }

    private void Move() {
        float h = Input.GetAxisRaw("Horizontal"); // value is either -1 or +1
        float v = Input.GetAxisRaw("Vertical");

        if (GameManager.instance.is3d) { // move in 3d
            Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

            rb.velocity = Vector3.Normalize(m_Move) * moveSpeed + new Vector3(0f, rb.velocity.y, 0f);
        }
        else { // move only in 2d
            rb.velocity = new Vector3(h * moveSpeed, rb.velocity.y, 0f);
        }

    }

    private void Jump() {

        // jumping even if jump button was pressed 0.2s ago
        if (jumpPressedRemember > 0)
            jumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
            jumpPressedRemember = JUMP_PRESSED_REMBEMBER_TIME;

        // jumping even if was grounded 0.1s ago
        if (groundedRembember > 0)
            groundedRembember -= Time.deltaTime;
        if (IsGrounded())
            groundedRembember = GROUNDED_REMEMBER_TIME;

        // process jump
        if ((groundedRembember > 0 && jumpPressedRemember > 0) || (currentJump < MAX_CONSECUTIVE_JUMPS && Input.GetButtonDown("Jump"))) {
            currentJump++;
            FindObjectOfType<AudioManager>().PlaySFX(jumpSfxIndex); // play jump SFX
            jumpPressedRemember = 0f;
            groundedRembember = 0f;

            if (!GameManager.instance.isGravityInverted)
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // if gravity normal, jump upward
            else
                rb.velocity = new Vector3(rb.velocity.x, -jumpForce, rb.velocity.z); // if gravity inverted, jump downward
        }

    }


    private bool IsGrounded() {

        if (!GameManager.instance.isGravityInverted)
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f); // if gravity normal, check if grounded below
        else
            return Physics.Raycast(transform.position, Vector3.up, distToGround + 0.1f); // if gravity normal, check if grounded above
    }

}


