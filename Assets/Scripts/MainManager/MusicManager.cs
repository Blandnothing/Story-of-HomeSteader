using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static MusicManager instance;
    public static MusicManager Instance { get { return instance; } }
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioSource musicSource;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    public void PlaySound(AudioClip audioClip)
    {
        soundSource.PlayOneShot(audioClip);
    }
    public void PlaySound(AudioClip audioClip,float volume)
    {
        soundSource.PlayOneShot(audioClip,volume);
    }
    public void PlayMusic(AudioClip audioClip)
    {
        musicSource.Pause();
        musicSource.clip = audioClip;
        musicSource.Play();
    }
}
