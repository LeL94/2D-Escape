using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock3d : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("3D skill unlocked!");
            PlayerPrefs.SetInt("3d_unlocked", 1);
            //Destroy(gameObject);
        }
    }
}
