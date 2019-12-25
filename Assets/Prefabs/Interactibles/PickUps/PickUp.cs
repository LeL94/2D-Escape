using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Collectable {
        
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player"))
            return;

        switch (gameObject.tag) {
            case "PickUpJump":
                PickUpJump();
                break;
            /*case "PickUpFollow":
                PickUpFollow();
                break;*/
            case "PickUpGravity":
                PickUpGravity();
                break;
            case "PickUp3d":
                PickUp3d();
                break;
        }

        FindObjectOfType<AudioManager>().PlaySFX(4); // play pickup SFX
        Destroy(gameObject);
    }

    private void PickUpJump() {
        GameManager.instance.EnableJump(true);
    }

    /*private void PickUpFollow() {
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<MainCamera>().StartFollowing();
    }*/

    private void PickUpGravity() {
        GameManager.instance.InvertGravity();
    }

    private void PickUp3d() {
        GameManager.instance.Switch3dView();
    }
}
