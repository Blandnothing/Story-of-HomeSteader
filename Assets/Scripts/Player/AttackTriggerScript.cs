using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerScript : MonoBehaviour
{

    Transform player;
    Animator animator;
    AnimatorStateInfo stateInfo;

    public float atkItemBack=1;
    public float playerSpeedInfectBack=1;


    void Start()
    {
        player = gameObject.transform.parent;
        animator = player.GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Enermy")
        {
            Vector2 v=other.transform.position-player.position;
            float h = Input.GetAxis("Horizontal");
            other.GetComponent<Rigidbody2D>().velocity = v*atkItemBack+Vector2.right*h*playerSpeedInfectBack;
        }
    }
}
