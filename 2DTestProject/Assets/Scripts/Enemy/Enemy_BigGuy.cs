using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BigGuy : Enemy, IDamageable
{
    public Transform PickUpPoint;
    public float power;

    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isDeadEnemy = true;
        }
        enemyAnim.SetTrigger("hit");
    }

    public void SetOff()//animation event
    {
        targetPoint.GetComponent<Bomb>()?.TurnOff();
    }
    
    public void PickUpBomb()
    {
        if(targetPoint.CompareTag("Bomb") && !hasBomb)
        {
            targetPoint.gameObject.transform.position = PickUpPoint.position;

            targetPoint.SetParent(PickUpPoint);

            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            hasBomb = true;
        }
    }

    public void ThrowAway()
    {
        if(hasBomb)
        {
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            targetPoint.SetParent(transform.parent.parent);

            if(FindObjectOfType<PlayerController>().gameObject.transform.position.x - transform.position.x < 0)
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * power, ForceMode2D.Impulse);
            }
            else
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * power, ForceMode2D.Impulse);
        }
        hasBomb = false;
    }
}
