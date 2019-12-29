using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard : TriggerListener
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private bool isTriggered = true;

    public Transform point1, point2;

    private Vector3 startPos, targetPos; // current start point (to handle Lerp movement)
    private float timePosition = 0f; // current time position (to handle Lerp movement)

    private void Start() {
        startPos = transform.position;
        targetPos = point2.position;
    }

    private void Update() {
        if (isTriggered)
            Move();
    }

    private void Move() {
        timePosition += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, targetPos, timePosition * moveSpeed);

        if (Vector3.Distance(transform.position, targetPos) <= 0.01f) {
            timePosition = 0;

            if (targetPos == point1.position) {
                startPos = point1.position;
                targetPos = point2.position;
            }
            else {
                startPos = point2.position;
                targetPos = point1.position;
            }
        }
    }

    override public void TriggerAction() {
        isTriggered = true;
    }
}
