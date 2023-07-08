using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneScript : MonoBehaviour
{
    public int numScene;
    public void MoveToMainTown()
    {
        SceneManager.LoadScene(numScene);
        UIManager.Instance.Clear();
    }
}
