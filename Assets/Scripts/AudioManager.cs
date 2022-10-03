using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource sourceMusic;
    [SerializeField] AudioSource sourceFX;
    [Space]
    [SerializeField] AudioClip battleLoop;
    [SerializeField] FXAudio sfx = new FXAudio();
    public static FXAudio SFX { get => _instance.sfx; }


    public static void Play(AudioClip clip, float volume = 1f)
    {
        _instance.sourceFX.PlayOneShot(clip, volume);
    }

    [System.Serializable]
    public class FXAudio
    {
        public AudioClip playerHit;
        public AudioClip shootBullet;

        public AudioClip textRollout;
    }
}
