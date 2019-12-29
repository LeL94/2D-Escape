using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour {

    [SerializeField] private float jumpForce = 15;
    [SerializeField] private int sfxIndex = 5;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            AudioManager.instance.PlaySFX(sfxIndex);
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (GameManager.instance.isGravityInverted)
                rb.velocity = new Vector3(rb.velocity.x, -jumpForce, rb.velocity.z);
            else
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }        
    }
}
