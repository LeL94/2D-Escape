using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] float lifetime = 2f;

    private void Start() {
        Destroy(gameObject, lifetime);
    }
}
