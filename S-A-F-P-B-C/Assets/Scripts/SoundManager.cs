using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance { get; private set; }
    void Awake() => instance = this;

    
    [SerializeField] private AudioSource audioSource;
    
    public GameSounds gameSounds;
    
    [Serializable]
    public class GameSounds
    {
        [SerializeField] public AudioClip cardFlipSound;
        [SerializeField] public AudioClip matchSound;
        [SerializeField] public AudioClip missMatchSound;
        [SerializeField] public AudioClip winSound;
    }

    public void AudioPlay(AudioClip soundClip)
    {
        audioSource.clip = soundClip;
        audioSource.Play();
    }
    
}
