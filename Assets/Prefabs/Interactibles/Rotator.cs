using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed_x = 30f;
    [SerializeField] private float rotationSpeed_y = 30f;
    [SerializeField] private float rotationSpeed_z = 30f;


    private void Update() {
        transform.Rotate(rotationSpeed_x * Time.deltaTime,
        rotationSpeed_y * Time.deltaTime,
        rotationSpeed_z * Time.deltaTime);
    }
}
