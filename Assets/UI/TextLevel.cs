using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevel : MonoBehaviour {

    private void Start() {
        GetComponent<Text>().text = GameController.getCurrentLevel().ToString();
    }
}
