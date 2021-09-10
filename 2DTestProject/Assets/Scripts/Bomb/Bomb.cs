using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator bombAnim;
    private Collider2D coll;
    private Rigidbody2D rbBoob;

    public float startTime;
    public float waitTime;
    public float bombForce;

    [Header("Check")]
    public float radius;
    public LayerMask targetLayer;

    void Start()
    {
        bombAnim = GetComponent<Animator>();    
        coll = GetComponent<Collider2D>();
        rbBoob = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    void Update()
    {
        if(!bombAnim.GetCurrentAnimatorStateInfo(0).IsName("Bomb_off"))
        {
            if (Time.time > startTime + waitTime)
            {
                bombAnim.Play("Bomb_explosion");
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Explosion() //Animation event, check the starting time and ending time of the bomb.
    {
        coll.enabled = false;
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        rbBoob.gravityScale = 0;
        
        foreach (var item in aroundObjects)
        {
            Vector3 objPos = transform.position - item.transform.position;

            item.GetComponent<Rigidbody2D>().AddForce((-objPos + Vector3.up) * bombForce, ForceMode2D.Impulse);

            //点燃其他炸弹的方法  炸弹攻击方式 
            if(item.CompareTag("Bomb")&& item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Bomb_off"))
            {
                item.GetComponent<Bomb>().TurnOn();
            }

            if (item.CompareTag("Player"))
                //Debug.Log("Bomb attack player");
                item.GetComponent<IDamageable>().GetHit(3);

            if (item.CompareTag("Enemy"))
                item.GetComponent<IDamageable>().GetHit(3);
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void TurnOff()
    {
        bombAnim.Play("Bomb_off");
        gameObject.layer = LayerMask.NameToLayer("NPC");
    }

    public void TurnOn()
    {
        startTime = Time.time;
        bombAnim.Play("Bomb_on");
        gameObject.layer = LayerMask.NameToLayer("Bomb");
    }


}
