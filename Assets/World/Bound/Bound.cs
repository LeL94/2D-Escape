using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        GameController.loadRelativeLevel(0); // reset level
    }
}
