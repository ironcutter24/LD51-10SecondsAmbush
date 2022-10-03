using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource sourceA;
    [SerializeField] AudioSource sourceB;
    [SerializeField] AudioSource sourceFX;
    [Space]
    [SerializeField] AudioClip music_intro;
    [SerializeField] AudioClip music_loop;
    [SerializeField] FXAudio sfx = new FXAudio();
    public static FXAudio SFX { get => _instance.sfx; }

    public static void Play(AudioClip clip, float volume = 1f)
    {
        _instance.sourceFX.PlayOneShot(clip, volume);
    }

    public void PlayMusic()
    {
        PlayIntroAndLoop(music_intro, music_loop);
    }

    private void PlayIntroAndLoop(AudioClip _introClip, AudioClip _loopClip)
    {
        StopMusic();

        sourceA.clip = _introClip;
        sourceA.volume = 1f;
        sourceA.loop = false;

        sourceB.clip = _loopClip;
        sourceB.volume = 1f;
        sourceB.loop = true;

        sourceA.Play();
        sourceB.PlayDelayed(_introClip.length);
    }

    public void StopMusic()
    {
        sourceA.Stop();
        sourceB.Stop();
    }

    [System.Serializable]
    public class FXAudio
    {
        public AudioClip playerHit;
        public AudioClip shootBullet;

        public AudioClip textRollout;
    }
}
