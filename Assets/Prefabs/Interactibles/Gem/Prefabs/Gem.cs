using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{   
    [SerializeField] private int sfxIndex = 4;
    public enum GemIndex { Gem1, Gem2, Gem3 }; // TODO distinguish gem types
    public GemIndex thisGemIndex;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(sfxIndex);
            GameManager.instance.SaveGem(thisGemIndex); // record in player prefs that this gem has been taken
        }
            
    }
}
