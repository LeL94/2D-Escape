using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    public Transform point1, point2;

    private Vector3 startPoint, targetPoint; // current start point (to handle Lerp movement)
    private float timePosition = 0f; // current time position (to handle Lerp movement)

    private void Start() {
        transform.position = point1.position; // move hazard at point 1
        startPoint = point1.position;
        targetPoint = point2.position;
    }

    private void Update() {
        Move();
    }

    private void Move() {
        timePosition += Time.deltaTime;
        transform.position = Vector3.Lerp(startPoint, targetPoint, timePosition * moveSpeed);

        if (Vector3.Distance(transform.position, targetPoint) <= 0.01f) {
            timePosition = 0;

            Vector3 temp = startPoint;
            startPoint = targetPoint;
            targetPoint = temp;
        }
    }
}
