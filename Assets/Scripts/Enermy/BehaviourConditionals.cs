using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using static BehaviorDesigner.Runtime.BehaviorManager;

public class IsPlayerInSight : Conditional
{
    public SharedGameObject player;
    public SharedFloat viewRange;
    public override TaskStatus OnUpdate()
    {
        if (player == null) Debug.LogError("player²»´æÔÚ");
        float distance = Vector2.Distance(transform.position,player.Value.transform.position);
        if (distance<viewRange.Value)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }    
}
public class MoveTowardPlayer : Action
{
    public SharedGameObject player;
    Rigidbody2D rigidbody;
    Animator animator;
    public SharedFloat moveSpeed;
    public SharedFloat attackDistance;
    public override void OnAwake()
    {
        rigidbody=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }
    public override TaskStatus OnUpdate()
    {
        if (transform.position.x<player.Value.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX=false;
            rigidbody.velocity = new Vector2(moveSpeed.Value,rigidbody.velocity.y);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            rigidbody.velocity = new Vector2(-moveSpeed.Value,rigidbody.velocity.y);
        }
        if (Mathf.Abs(transform.position.x-player.Value.transform.position.x)<attackDistance.Value)
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
        animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));

        return TaskStatus.Success;
    }
}
public class Wander : Action
{
    public SharedFloat moveSpeed;
    public SharedFloat changeDirectionTime;
    SharedInt direction = 1;
    float changeDirectionTimer;
    Animator animator;
    Rigidbody2D rigidbody;
    Transform groundSensor;
    public override void OnAwake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundSensor = transform.Find("GroundSensor");
    }
    public override TaskStatus OnUpdate()
    {
        changeDirectionTimer+=Time.deltaTime;
        if (changeDirectionTimer > changeDirectionTime.Value) ChangeDirection();

        rigidbody.velocity = new Vector2(moveSpeed.Value * direction.Value, rigidbody.velocity.y);
        if (direction.Value==1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true ;
        }

        animator.SetFloat("Speed",Mathf.Abs(rigidbody.velocity.x));

        return TaskStatus.Running;
    }
    void ChangeDirection()
    {
        direction.Value *= -1;
        changeDirectionTimer-=changeDirectionTime.Value;
    }
}
public class Attack : Action
{
    Animator animator;
    public SharedGameObject player;
    public SharedFloat attackDistance;
    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
    }
    public override TaskStatus OnUpdate()
    {
        if ((player.Value.transform.position - transform.position).magnitude <= attackDistance.Value)
        {
            animator.SetTrigger("Attack");
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}