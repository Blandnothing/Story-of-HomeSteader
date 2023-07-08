using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BehaviorDesigner.Runtime;

public class EnermyBaseScript : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float attackPower;
    public float criticalRate;
    public float criticalDamage;
    public float invincibleTime;
    protected Vector2 direction;
    protected bool isHit;
    protected AnimatorStateInfo animStateInfo;
    protected Animator animator; 
    protected Rigidbody2D rb2d;
    protected GameObject damageText;
    protected float duration = 1f;
    protected float range=1f;
    protected float jumpPower = 2f;
    [SerializeField] Slider healthBar;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        health=maxHealth;
        damageText = Resources.Load<GameObject>("Prefabs/UI/DamageText");
        if (damageText==null)
        {
            Debug.LogError("damageTextÎ´¼ÓÔØ");
        }
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

    public void GetHit(Vector2 direction,float attackPower,float criticalRate,float criticalDamage)
    {
        if (direction.x == 1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        isHit = true;
        this.direction = direction;
        animator.SetTrigger("Hit");

        if (Random.value<=criticalRate)
        {
            health -= attackPower * (1 + criticalDamage);
            TextMeshProUGUI damageGui = Instantiate(damageText, transform.Find("StatusCanvas"), false).GetComponent<TextMeshProUGUI>();
            damageGui.text= (-(attackPower * (1 + criticalDamage))).ToString();
            damageGui.color = Color.red;
            damageGui.fontSize = 2;
        }
        else
        {
            health -= attackPower;
            TextMeshProUGUI damageGui = Instantiate(damageText, transform.Find("StatusCanvas"), false).GetComponent<TextMeshProUGUI>();
            damageGui.text = (-(attackPower)).ToString();
            damageGui.color = Color.white;
        }
        healthBar.value = health / maxHealth;
        if (health<=0)
        {
            animator.SetTrigger("Dead");
            GetComponent<BehaviorTree>().DisableBehavior();
            transform.Find("StatusCanvas/HealthBar").gameObject.SetActive(false);
        }
        StopCoroutine(Invincible());
        StartCoroutine(Invincible());
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
    IEnumerator Invincible()
    {
        gameObject.layer = LayerMask.NameToLayer("invincible");
        yield return new WaitForSeconds(invincibleTime);
        gameObject.layer = LayerMask.NameToLayer("Enermy");
    }
    protected virtual void OnDestroy()
    {
        
    }
}
