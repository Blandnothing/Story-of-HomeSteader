using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointMoveScript : MonoBehaviour
{
    public float speed=200;
    private float inputX;
    private float inputY;
    private Rigidbody2D rb2d;
    private void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rb2d.velocity=new Vector2(inputX,inputY)*speed;
    }
}
