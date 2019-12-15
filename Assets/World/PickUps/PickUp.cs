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
        GameController.enableJump(true);
    }

    /*private void PickUpFollow() {
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<MainCamera>().StartFollowing();
    }*/

    private void PickUpGravity() {
        GameController.invertGravity();
    }

    private void PickUp3d() {
        GameController.enable3d();
    }
}
