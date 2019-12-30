using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{
    public static LSUIManager instance;

    public GameObject levelInfoPanel, levelLockedPanel;
    public Text levelSelectedText, levelLockedText, gemsTakenText;

    private void Awake() {
        instance = this;
    }
}
