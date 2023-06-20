using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyBaseScript : MonoBehaviour
{
    public float speed;
    protected Vector2 direction;
    protected bool isHit;
    protected AnimatorStateInfo animStateInfo;
    protected Animator animator; 
    protected Rigidbody2D rb2d;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (isHit)
        {
            rb2d.velocity = direction * speed;
            if (animStateInfo.normalizedTime>=.6f)
            {
                isHit = false;
            }
        }
    }

    public void GetHit(Vector2 direction)
    {
        transform.localScale = new Vector3(-direction.x, 1, 1);
        isHit= true;
        this.direction = direction;
        animator.SetTrigger("Hit");
    }
}
