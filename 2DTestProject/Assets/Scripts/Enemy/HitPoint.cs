using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public bool bombAvaliable;
    int direction;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > collision.transform.position.x )
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player get hurt");
            collision.GetComponent<IDamageable>().GetHit(1);
        }

        if(collision.CompareTag("Bomb") && bombAvaliable)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction, 1) * 10, ForceMode2D.Impulse);
        }

    
    }
}
