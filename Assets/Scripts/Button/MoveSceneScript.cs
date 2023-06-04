using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneScript : MonoBehaviour
{
    public void MoveToMainTown()
    {
        SceneManager.LoadScene(1);
    }
}
