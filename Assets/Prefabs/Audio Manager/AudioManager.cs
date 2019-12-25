using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] musicArray;
    [SerializeField] private int backingTrackIndex = 0;
    public AudioSource[] sfxArray;
    

    private void Awake() {
        // Singleton pattern
        int numGameController = FindObjectsOfType<AudioManager>().Length;
        if (numGameController > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        PlayMusic(backingTrackIndex);
    }

    public void PlayMusic(int musicToPlay) {
        // first stop all music
        foreach (AudioSource music in musicArray)
            music.Stop();
        musicArray[musicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay) {
        sfxArray[sfxToPlay].Play();
    }
}
