using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyBaseState currentState;  // FSM state switch var

    public Animator enemyAnim;
    public int enemyAnimState;

    private GameObject alarmSign;

    [Header("Base State")]
    public float health;
    public bool isDeadEnemy;
    public bool hasBomb; // Use for the BigGuy to define bomb on his hands

    public bool isBoss;

    //Movement state
    [Header("Movement")]
    public float speed;
    public Transform pointA, pointB;
    public Transform targetPoint;

    //Attack State
    [Header("Attack Setting")]
    public float attackRate,skillRate;
    public float attackRange,skillRange;
    private float nextAttack = 0;


    public List<Transform> attackList = new List<Transform>();// List for searching Player or Bombs in the range

    public PatrolState patrolState = new PatrolState();// FSM state
    public AttackState attackState = new AttackState();// FSM state


    public virtual void Init()
    {
        enemyAnim = GetComponent<Animator>();
        alarmSign = transform.GetChild(0).gameObject;

        //GameManager.instance.IsEnemy(this);
    }

    public void Awake()
    {
        Init();
    }

    void Start()
    {
        TransitionToState(patrolState);
        GameManager.instance.IsEnemy(this);
        //GameManager.instance.AddEnemy(this);
    }

    void Update()
    {
        enemyAnim.SetBool("dead", isDeadEnemy);
        if (isDeadEnemy)
        {
            GameManager.instance.EnemyDead(this);
            return;
        }

        currentState.OnUpdate(this);
        enemyAnim.SetInteger("state", enemyAnimState);

    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);

    }

    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FlipDirection();
    }

    public void AttackAction() // Attack Player
    {
        //Debug.Log("Base attack");
        
        if(Vector2.Distance(transform.position,targetPoint.position) < attackRange)
        {
            if(Time.time > nextAttack)
            {
                //Play the attack animation
                enemyAnim.SetTrigger("attack");

                Debug.Log("Base attack");
                nextAttack = Time.time + attackRate;
            }
        }
    }

    public virtual void SkillAction() //Use skill to the bomb , virtual允许子类访问
    {
        //Debug.Log("Skill attack");

        if (Vector2.Distance(transform.position, targetPoint.position) < skillRange)
        {
            if (Time.time > nextAttack)
            {
                //Play the skill animation
                enemyAnim.SetTrigger("skill");  //Animator Skill trigger
                Debug.Log("Skill attack");
                nextAttack = Time.time + skillRate;
            }
        }
    }

    public void FlipDirection()//flip enemy direction
    {
        if (transform.position.x < targetPoint.position.x)
            transform.rotation = Quaternion.Euler(0f,0f,0f);
        else
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    public void SwitchPoint()
    {
        if(Mathf.Abs(pointA.position.x - transform.position.x)> Mathf.Abs(pointB.position.x - transform.position.x))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = pointB;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //bug area1
        if(!attackList.Contains(collision.transform) && !hasBomb && !isDeadEnemy && !GameManager.instance.gameOver)
            attackList.Add(collision.transform);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isDeadEnemy && !GameManager.instance.gameOver)
            StartCoroutine(OnAlarm());
    }
    
    IEnumerator OnAlarm()
    {
        alarmSign.SetActive(true);
        yield return new WaitForSeconds(alarmSign.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        alarmSign.SetActive(false);
    }

}
