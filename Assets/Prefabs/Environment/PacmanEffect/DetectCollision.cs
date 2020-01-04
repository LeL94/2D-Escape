using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectCollision : MonoBehaviour {
    public enum PacmanBlock { left, right, above, below };
    [SerializeField] private PacmanBlock pacmanBlockType;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            ContactPoint contactPoint = collision.contacts[0];
            Vector3 contactPos = contactPoint.point;
            //Debug.Log("player contact with block " + pacmanBlockType + " at pos " + contactPos);

            switch (pacmanBlockType) {
                case PacmanBlock.left:
                    PacmanEffect.instance.TeleportToOppositeBlock(PacmanBlock.right, contactPos);                    
                    break;
                case PacmanBlock.right:
                    PacmanEffect.instance.TeleportToOppositeBlock(PacmanBlock.left, contactPos);
                    break;
                case PacmanBlock.below:
                    PacmanEffect.instance.TeleportToOppositeBlock(PacmanBlock.above, contactPos);
                    break;

            }
            
        }
    }
}
