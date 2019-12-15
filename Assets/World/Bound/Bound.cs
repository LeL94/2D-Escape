using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
            GameController.loadRelativeLevel(0); // reset level
    }
}
