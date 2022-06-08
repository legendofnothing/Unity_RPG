using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager { get; private set; }

    public AudioSource audioSource;
    public AudioClip[] soundEffects;
    public AudioClip[] soundTracks;

    private void Awake() {
        if (manager != null && manager != this) {
            Destroy(this);
        }

        else manager = this;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(string name) {
        for (int i = 0; i < soundEffects.Length; i++){
            if(soundEffects[i].name == name) {
                audioSource.PlayOneShot(soundEffects[i], 0.1f);
            }

        }
    }

    public void PlayTrack(string name) {
        for (int i = 0; i < soundTracks.Length; i++) {
            if (soundTracks[i].name == name) {
                audioSource.clip = soundTracks[i];
                audioSource.Play();
            }

        }
    }
}
