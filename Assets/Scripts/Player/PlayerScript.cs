using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    //ÒÆ¶¯
    public Transform groundCheck;
    public LayerMask ground;
    private bool isGround;
    private float inputX;
    private float graceTimer;
    [SerializeField] float graceTime;
    [SerializeField] float m_speed = 4.0f;
    //ÌøÔ¾
    public int maxJumpCount = 1;
    private int jumpCount;
    private bool jumpPressed;
    private bool isJump;
    [SerializeField] float m_jumpForce = 7.5f;
    //ÉúÃü
    public float maxHealth=100;
    float currentHealth;
    [SerializeField] Slider sliderHealth;
    [SerializeField] TextMeshProUGUI textHealth;

    enum stateID
    {

    }


    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        textHealth.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // -- Handle input and movement --
        inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        

        if (Input.GetButtonDown("Jump") && jumpCount>0)
        {
            jumpPressed=true;
        }
       // --Handle Animations--

        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
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
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
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
        if(jumpPressed && (isGround  || graceTimer>0))
        {
            isJump = true;
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            jumpCount--;
            jumpPressed = false;
            graceTimer = 0;
        }else if(jumpPressed && jumpCount > 0 && isJump)
        {
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            jumpCount--;
            jumpPressed = false;
        } 
    }
    void SwitchAnim()
    {
        m_animator.SetFloat("SpeedX",Mathf.Abs(inputX));
        if (isGround)
        {
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
    public void changeHealth(float amount)
    {
        if (amount<0)
        {
            m_animator.SetTrigger("Hurt");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if(currentHealth < 0)
        {
            m_animator.SetTrigger("Death");
        }sliderHealth.value = currentHealth/maxHealth;
        textHealth.text=currentHealth.ToString()+"/"+maxHealth.ToString();
    }

}