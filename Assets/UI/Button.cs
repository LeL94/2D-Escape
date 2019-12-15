using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public void loadLevel(int level) {
        GameController.loadRelativeLevel(level);
    }

    public void enableJump(bool canJump) {
        GameController.enableJump(true);
    }

    public void enable3d(bool is3d) {
        GameController.enable3d();
    }

    public void invertGravity() {
        GameController.invertGravity();
    }
}
