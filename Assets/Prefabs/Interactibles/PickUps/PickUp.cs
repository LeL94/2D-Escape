﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private enum PickupType {PickupJump, PickupFollow, PickupGravity, Pickup3d};
    [SerializeField] private PickupType pickupType;
        
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player"))
            return;

        switch (pickupType) {
            case PickupType.PickupJump:
                PickUpJump();
                break;
            /*case Pickup.PickupFollow:
                PickUpFollow();
                break;*/
            case PickupType.PickupGravity:
                PickUpGravity();
                break;
            case PickupType.Pickup3d:
                PickUp3d();
                break;
        }

        FindObjectOfType<AudioManager>().PlaySFX(3); // play pickup SFX
        gameObject.SetActive(false); // don't destroy, so it can be reactivated by game manager with RespawnPickups()
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
