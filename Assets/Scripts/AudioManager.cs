using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource sourceMusic;
    [SerializeField] AudioSource sourceFX;
    [SerializeField] AudioSource sourceUI;
    [Space]
    [SerializeField] MusicAudio Music = new MusicAudio();
    [SerializeField] FXAudio FX = new FXAudio();
    [SerializeField] UIAudio UI = new UIAudio();


    [System.Serializable]
    public class MusicAudio : AudioCategory
    {
        public AudioClip forestAmbience;
        public AudioClip battleLoop;
    }

    [System.Serializable]
    public class FXAudio : AudioCategory
    {
        public AudioClip playerHit;
    }

    [System.Serializable]
    public class UIAudio : AudioCategory
    {
        public AudioClip textRollout;
    }

    [System.Serializable]
    public class AudioCategory
    {

    }
}
