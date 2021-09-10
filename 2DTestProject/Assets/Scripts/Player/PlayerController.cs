using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rbPlayer;
    private Animator animPlayer;
    private FixedJoystick joyStick;

    public float speedPlayer;
    public float jumpForcePlayer;

    
    //Playre state , health .etc
    [Header("Player State")]
    public float health;
    public bool isDead;

    //Player movement var
    //*************************
    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("State Check")]
    public bool isGround;
    public bool isJump;
    public bool canJump;

    [Header("Jump FX")]
    public GameObject jumpFX;
    public GameObject landFX;
    //**************************

    [Header("Attack Settings")]
    public GameObject bombPrefab;
    public float nextAttack = 0;
    public float attackRate;



    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();

        joyStick = FindObjectOfType<FixedJoystick>();

    }

    void Update()
    {
        


        animPlayer.SetBool("dead", isDead);
        if (isDead)
            return;

        CheckInput();
    }

    public void FixedUpdate()
    {
        if(isDead)
        {
            rbPlayer.velocity = Vector2.zero;
            return;
        }
        PhysicsCheck();
        Movement();
        Jump();
    }

    void CheckInput()
    {

        if (Input.GetButtonDown("Jump") && isGround)
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }


    // Player movement & Collisions
    void Movement()
    {
        //Keyboard Control
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // not include float.
        // GetAxis -1~ +1 include float

        //float horizontalInput = joyStick.Horizontal;

        rbPlayer.velocity = new Vector2(horizontalInput * speedPlayer, rbPlayer.velocity.y);

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }

        //if (horizontalInput > 0)
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //if (horizontalInput < 0)
        //    transform.eulerAngles = new Vector3(0, 180, 0);
        

    }

    void Jump()
    {
        if (canJump)
        {
            isJump = true;
            jumpFX.SetActive(true);
            jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpForcePlayer);
            //rbPlayer.gravityScale = 5;
            canJump = false;
        }

    }

    public void ButtonJump()
    {
        canJump = true;
    }


    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGround)
        {
            rbPlayer.gravityScale = 1;
            isJump = false;
        }
        else
        {
            rbPlayer.gravityScale = 5;
        }
    }

    public void LandFX()//anim event
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }

    //Player attack
    public void Attack()
    {
        if (Time.time > nextAttack)
        {
            Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);

            nextAttack = Time.time + attackRate;
        }
    }
     
    void IDamageable.GetHit(float damage)
    {
        if(!animPlayer.GetCurrentAnimatorStateInfo(1).IsName("player_Hit"))
        {
            //health = health - damage;
            health -= damage;
            if (health < 1)
            {
                health = 0;
                isDead = true;
            }
            animPlayer.SetTrigger("hit");

            UIManager.instance.UpdateHealth(health);
        }
        
    }
}
