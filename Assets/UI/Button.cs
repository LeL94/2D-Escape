using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    public void nextLevel() {
        GameController.loadNextLevel();
    }

    public void previousLevel() {
        GameController.loadPreviousLevel();
    }
}
