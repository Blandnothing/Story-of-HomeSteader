using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestScript : MonoBehaviour
{
    bool isOpened;
    Animator animator;
    public GameObject coin;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpened && collision.CompareTag("Player"))
        {
            isOpened = true;
            animator.SetBool("IsOpened",isOpened);
            Instantiate(coin,transform.position+Vector3.up*0.5f,transform.rotation);
        }
    }
}
