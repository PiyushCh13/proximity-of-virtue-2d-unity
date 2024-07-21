using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioSource musicSource;

    [SerializeField] public AudioClip bossBattle;
    [SerializeField] public AudioClip LevelVictory;
    [SerializeField] public AudioClip mainLevel;
    [SerializeField] public AudioClip menuSong;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;

        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

}
