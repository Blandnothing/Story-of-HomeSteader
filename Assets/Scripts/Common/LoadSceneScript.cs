using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public int numScene;
    public AudioClip soundClip;
    public float volume;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (soundClip != null)
                MusicManager.Instance.PlaySound(soundClip,volume);
            SceneManager.LoadScene(numScene);
            UIManager.Instance.Clear();
        }
    }
}
