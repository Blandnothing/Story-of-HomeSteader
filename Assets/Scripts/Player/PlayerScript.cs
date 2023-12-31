using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;
using Cinemachine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class PlayerScript : MonoBehaviour
{
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    [SerializeField] CinemachineImpulseSource impulseSource;
   
    //�ƶ�
    public Transform groundCheck;
    public LayerMask ground;
    private bool isGround;
    private float inputX;
    private float graceTimer;
    [SerializeField] float graceTime;
    [JsonProperty] [SerializeField] float m_speed = 4.0f;
    //��Ծ
    [JsonProperty] public int maxJumpCount = 2;
    private int jumpCount;
    private bool jumpPressed;
    private bool isJump;
    [SerializeField] float m_jumpForce = 7.5f;
    //����
    [JsonProperty] public float maxHealth=100;
    [JsonProperty] float currentHealth;
    [SerializeField] Slider sliderHealth;
    [SerializeField] TextMeshProUGUI textHealth;
    bool isDeath;
    //����
    [JsonProperty] public float attackPower = 5;
    [JsonProperty] public float criticalRate = 0.05f;
    [JsonProperty] public float criticalDamage = 0.5f;
    [JsonProperty] public float attackSpeed = 1;
    public float invincibleTime;
    bool isAttack;
    public float attackBehind=0.2f;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    //����
    [HideInInspector] public bool isInteracted;

    //��Ч
    [SerializeField] AudioClip moveSoundClip1;   //11
    [SerializeField] AudioClip moveSoundClip2;   //12
    [SerializeField] AudioClip attackSoundClip1; //21
    [SerializeField] AudioClip attackSoundClip2; //22
    [SerializeField] AudioClip attackSoundClip3; //23
    [SerializeField] AudioClip deathSoundClip; //3
    [SerializeField] AudioClip jumpSoundClip;  //4
    [SerializeField] AudioClip jumpLandSoundClip; //5
    [SerializeField] AudioClip hitSoundClip;  //6
    [SerializeField] AudioClip swordHitSoundClip;  //7
    


    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        if (sliderHealth==null)
        {
            Transform healthPanel = GameObject.Find("HealthPanel").transform;
            sliderHealth = healthPanel.Find("Slider").gameObject.GetComponent<Slider>();
            textHealth=healthPanel.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        }
        textHealth.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    void Update()
    {
        if (isDeath) return;

        m_timeSinceAttack += Time.deltaTime;

        inputX = Input.GetAxis("Horizontal");

        

        if (Input.GetButtonDown("Jump") && jumpCount>0)
        {
            jumpPressed=true;
        }

        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !isAttack)
        {
            StopCoroutine(Attack());
            StartCoroutine(Attack());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            isInteracted=true;
        }
    }
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.05f, ground);
        GroundMovement();
        Jump();

        SwitchAnim();
    }
    void GroundMovement()
    {
        if (isAttack)
        {
            m_body2d.velocity = new Vector2(transform.localScale.x * attackSpeed, m_body2d.velocity.y);
        }
        else
        {
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }
        if (inputX > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }

        else if (inputX < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    void Jump()
    {
        if (isGround)
        {
            jumpCount = maxJumpCount;
            isJump = false;
            graceTimer = graceTime;
        }
        else
        {
            graceTimer-=Time.fixedDeltaTime;
        }
        if (isAttack)
        {
            jumpPressed = false;
            return;
        }
        if (jumpPressed && (isGround  || graceTimer>0))
        {
            PlayMoveSound(4);
            isJump = true;
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            jumpCount--;
            jumpPressed = false;
            graceTimer = 0;
        }else if(jumpPressed && jumpCount > 0 && isJump)
        {
            PlayMoveSound(4);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            jumpCount--;
            jumpPressed = false;
        } 
    }
    IEnumerator  Attack()
    {
        float startTime=Time.time;
        isAttack = true;

        m_currentAttack++;
        if (m_currentAttack > 3)
            m_currentAttack = 1;
        if (m_timeSinceAttack > 1.0f)
            m_currentAttack = 1;
        m_animator.SetTrigger("Attack" + m_currentAttack);
        m_timeSinceAttack = 0.0f;

       

        while(Time.time - startTime < attackBehind)
        {
            if(Time.time-startTime>attackBehind-0.05f)
                isAttack = false;
            m_body2d.velocity = Vector2.zero;
            yield return  null; 
        }


        isAttack = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enermy"))
        {
            PlayMoveSound(7);
            other.GetComponent<EnermyBaseScript>().GetHit(transform.localScale,attackPower,criticalRate,criticalDamage);
        }
    }
    void SwitchAnim()
    {
        m_animator.SetFloat("SpeedX",Mathf.Abs(inputX));
        if (isGround)
        {
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            {
                PlayMoveSound(5);
            }
            m_animator.SetBool("Falling", false);
            m_body2d.gravityScale = 1;
        }        
        else if (!isGround && m_body2d.velocity.y > 0) {
            m_animator.SetBool("Jump", true);
            m_body2d.gravityScale = 1;
            graceTimer = 0;
        }    
        else if (m_body2d.velocity.y<0)
        {
            m_animator.SetBool("Jump",false);
            m_animator.SetBool("Falling", true);
            m_body2d.gravityScale = 2;
        }
    }
    public void ChangeHealth(float amount)
    {
        if (amount<0)
        {
            m_animator.SetTrigger("Hurt");
            StopCoroutine(Invincible());
            StartCoroutine(Invincible());
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if(currentHealth <= 0)
        {
            isDeath = true;
            m_animator.SetTrigger("Death");
        }sliderHealth.value = currentHealth/maxHealth;
        textHealth.text=currentHealth.ToString()+"/"+maxHealth.ToString();
    }

    void PlayMoveSound(int num)
    {
        switch (num)
        {
            case 11:
                MusicManager.Instance.PlaySound(moveSoundClip1);
                break;
            case 12:
                MusicManager.Instance.PlaySound(moveSoundClip2);
                break;
            case 21:
                MusicManager.Instance.PlaySound(attackSoundClip1);
                break;
            case 22:
                MusicManager.Instance.PlaySound(attackSoundClip2);
                break;
            case 23:
                MusicManager.Instance.PlaySound(attackSoundClip3);
                break;
            case 3:
                MusicManager.Instance.PlaySound(deathSoundClip);
                break;
            case 4:
                MusicManager.Instance.PlaySound(jumpSoundClip);
                break;
            case 5:
                MusicManager.Instance.PlaySound(jumpLandSoundClip);
                break;
            case 6:
                MusicManager.Instance.PlaySound(hitSoundClip);
                break;
            case 7:
                MusicManager.Instance.PlaySound(swordHitSoundClip);
                break;
            default:
                break;
        }
        
    }
    IEnumerator Invincible()
    {
        gameObject.layer = LayerMask.NameToLayer("invincible");
        yield return new WaitForSeconds(invincibleTime);
        gameObject.layer = LayerMask.NameToLayer("player");
    } 
    void Death()
    {
        SceneManager.LoadScene(0);
    }
    public void UpdateInfo(LocalPlayerData.PlayerData.PlayerInfo player)
    {
        maxHealth=player.maxHealth;
        currentHealth = player.currentHealth;
        attackPower = player.attackPower;
        criticalDamage = player.criticalDamage;
        criticalRate = player.criticalRate;
    }
    public LocalPlayerData.PlayerData.PlayerInfo GetPlayerInfo()
    {
        return new LocalPlayerData.PlayerData.PlayerInfo(maxHealth, currentHealth, attackPower, criticalDamage, criticalRate);  
    }
}