using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    [SerializeField] private int sfxIndex = 6;

    public TriggerListener triggerListener;

    private bool isPressed = false;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !isPressed) {
            isPressed = true;
            transform.localPosition = Vector3.zero;
            AudioManager.instance.PlaySFX(sfxIndex);

            // trigger action
            triggerListener.TriggerAction();
        }
    }
}
