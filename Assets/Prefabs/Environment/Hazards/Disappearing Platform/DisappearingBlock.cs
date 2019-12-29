using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBlock : MonoBehaviour
{
    [SerializeField] private float ACTIVE_TIME = 2f;
    [SerializeField] private float FLASHING_REPETITIONS = 5;
    [SerializeField] private float FLASHING_TIME = 0.3f;
    [SerializeField] private float INACTIVE_TIME = 2f;

    private GameObject blocks;

    private int currentRepetition;

    private float activeTimer;
    private float flashingTimer;
    private float inactiveTimer;

    private enum DisappearingBlockState { active, flashing, inactive };
    private DisappearingBlockState currentState;


    private void Start() {
        blocks = gameObject.transform.GetChild(0).gameObject; // reference to blocks

        currentRepetition = 0;

        activeTimer = ACTIVE_TIME;
        flashingTimer = FLASHING_TIME;
        inactiveTimer = INACTIVE_TIME;
        currentState = DisappearingBlockState.active;
    }

    private void Update() {

        switch (currentState) {
            case DisappearingBlockState.active:
                StayActive();
                break;
            case DisappearingBlockState.flashing:
                Flash();
                break;
            case DisappearingBlockState.inactive:
                StayInactive();
                break;
        }        
    }


    private void StayActive() {
        GetComponent<Collider>().enabled = true;
        blocks.SetActive(true);

        activeTimer -= Time.deltaTime;

        if (activeTimer <= 0) {
            activeTimer = ACTIVE_TIME;
            currentState = DisappearingBlockState.flashing;
        }
    }


    private void Flash() {
        flashingTimer -= Time.deltaTime;

        if (flashingTimer <= 0) {
            flashingTimer = FLASHING_TIME;
            blocks.SetActive(!blocks.activeSelf); // flash blocks

            currentRepetition++;

            if (currentRepetition > FLASHING_REPETITIONS) {
                currentRepetition = 0;
                currentState = DisappearingBlockState.inactive;
            }

        }
    }

    private void StayInactive() {
        GetComponent<Collider>().enabled = false;
        blocks.SetActive(false);

        inactiveTimer -= Time.deltaTime;

        if (inactiveTimer <= 0) {
            inactiveTimer = INACTIVE_TIME;
            currentState = DisappearingBlockState.active;
        }
    }

}
