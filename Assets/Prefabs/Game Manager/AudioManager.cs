using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] musicArray;
    public int backingTrackIndex;

    public AudioSource[] sfxArray;    

    private void Awake() {
        instance = this;
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
