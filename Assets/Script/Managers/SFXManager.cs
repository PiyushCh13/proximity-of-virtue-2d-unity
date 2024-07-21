using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SFXManager : Singleton<SFXManager>
{
    public AudioSource sfxAudioSource;

    [SerializeField] public AudioClip player_hurt;
    [SerializeField] public AudioClip player_Jump;
    [SerializeField] public AudioClip player_Death;
    [SerializeField] public AudioClip player_walk;
    [SerializeField] public AudioClip player_Attack_Hit;
    [SerializeField] public AudioClip clickSound;
    [SerializeField] public AudioClip enemyExplode;
    [SerializeField] public AudioClip bossImpact;
    [SerializeField] public AudioClip bossHit;
    [SerializeField] public AudioClip bossShot;

    void Start()
    {
        sfxAudioSource = GetComponent<AudioSource>();
        sfxAudioSource.enabled = true;
    }

    private void Update()
    {

    }
    public void PlaySound(AudioClip clip) 
    {
        sfxAudioSource.clip = clip;

        if (sfxAudioSource != null && !sfxAudioSource.isPlaying)
        {
            sfxAudioSource.Play();
        }
    }

    public void PlayOneShot(AudioClip clip) 
    {
        sfxAudioSource.PlayOneShot(clip);
    }
}
