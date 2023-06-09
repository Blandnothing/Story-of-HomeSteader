using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeTestScript : MonoBehaviour
{
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioClip m_Clip;
    public void TestVolume()
    {
        m_AudioSource.PlayOneShot(m_Clip);
    }
}
