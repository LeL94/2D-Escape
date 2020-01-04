using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class PacmanEffect : MonoBehaviour
{
    public static PacmanEffect instance;

    public GameObject blockLeft, blockRight, blockAbove, blockBelow;

    private Vector3 offset; // distance from destination block at which player is teleported

    private void Awake() {
        instance = this;
    }

    public void TeleportToOppositeBlock(DetectCollision.PacmanBlock destinationBlockType, Vector3 contactPos) {

        GameObject destinationBlock = null;

        switch (destinationBlockType) {
            case DetectCollision.PacmanBlock.left:
                destinationBlock = blockLeft;
                offset = new Vector3(1.5f, 0f, 0f);
                break;
            case DetectCollision.PacmanBlock.right:
                destinationBlock = blockRight;
                offset = new Vector3(-1.5f, 0f, 0f);
                break;
            case DetectCollision.PacmanBlock.above:
                destinationBlock = blockAbove;
                offset = new Vector3(0f, -1.5f, 0f);
                break;
            case DetectCollision.PacmanBlock.below:
                destinationBlock = blockBelow;
                offset = new Vector3(0f, 1.5f, 0f);
                break;
        }

        Vector3 destinationPos = destinationBlock.transform.position + offset;

        if (destinationBlockType == DetectCollision.PacmanBlock.left || destinationBlockType == DetectCollision.PacmanBlock.right) {
            destinationPos = new Vector3(destinationPos.x, contactPos.y, destinationPos.z); // teleport player at the correct height (contactPos)
        }
        else {
            destinationPos = new Vector3(contactPos.x, destinationPos.y, destinationPos.z); // teleport player at the correct x (contactPos)
        }

        PlayerController.instance.transform.position = destinationPos;
        FindObjectOfType<FreeLookCam>().transform.position = destinationPos;
    }
}
