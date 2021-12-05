using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayContorller : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;

    public float jumpForce;

   
    [Header("Ground Check")] 
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    
    [Header("States Check")] 
    public bool isGround;
    public bool canJump; 
    public bool isJump;

    [Header("Jump FX")] 
    public GameObject jumpFX;
    public GameObject landFX;
    public GameObject runFX;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    public void FixedUpdate()
    { 
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
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //-1~1

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(horizontalInput,  1, 1);
            runFX.transform.localScale = new Vector3(horizontalInput,  1, 1);
        }

        // float VerticalInput = Input.GetAxisRaw("Vertical");
        // rb.
    }

    void Jump()
    {
        if (canJump)
        {
            isJump = true;
            jumpFX.SetActive(true);
            jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.gravityScale = 4;
            canJump = false;
        }
    }

    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGround)
            rb.gravityScale = 1;
        isJump = false;
    }

    public void LandFX()
    {
        landFX.SetActive(true);
        landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
    }

    public void RunFX()
    {
        runFX.SetActive(true);
        if (rb.velocity.x > 0)
        {
            runFX.transform.position = transform.position + new Vector3(-0.67f, -0.45f, 0);
        }
        else
        {
            runFX.transform.position = transform.position + new Vector3(0.67f, -0.45f, 0);
        }
        
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position,checkRadius);
    }
}
