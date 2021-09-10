using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator animDoor;
    BoxCollider2D coll;

    void Start()
    {
        animDoor = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>(); 

        coll.enabled = false;   
    }
    public void OpenDoor()// GameManager to define the condition of opening the door
    {
        animDoor.Play("Door_Open");
        coll.enabled = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Go to next room
            GameManager.instance.NextLevel();
        }
    }
}
