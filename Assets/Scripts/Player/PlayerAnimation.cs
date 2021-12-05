using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private Rigidbody2D rb;
    private PlayContorller contorller;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        contorller = GetComponent<PlayContorller>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed",Mathf.Abs(rb.velocity.x));
        anim.SetBool("jump",contorller.isJump);
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("ground", contorller.isGround);
        
   
    }
}
