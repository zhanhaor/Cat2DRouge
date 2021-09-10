using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D playerRb;
    private PlayerController playerController;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        playerAnim.SetFloat("speed",Mathf.Abs (playerRb.velocity.x));
        playerAnim.SetFloat("velocityY",playerRb.velocity.y);
        playerAnim.SetBool("jump", playerController.isJump);
        playerAnim.SetBool("ground", playerController.isGround);

    }
}
