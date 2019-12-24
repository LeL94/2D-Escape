using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Collectable {
        
    private void OnTriggerEnter(Collider other) {
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
        
        Destroy(gameObject);
    }

    private void PickUpJump() {
        GameManager.instance.enableJump(true);
    }

    /*private void PickUpFollow() {
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<MainCamera>().StartFollowing();
    }*/

    private void PickUpGravity() {
        GameManager.instance.invertGravity();
    }

    private void PickUp3d() {
        GameManager.instance.enable3d();
    }
}
