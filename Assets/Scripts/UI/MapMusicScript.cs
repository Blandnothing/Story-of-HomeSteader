using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMusicScript : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    void Start()
    {
        MusicManager.Instance.PlayMusic(musicClip);
    }

}
