using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;
    private Rigidbody2D rb;
    public float startTime;
    public float waitTime;
    public float bombForce;
    [Header("check")] 
    public float radius;
    public LayerMask targetLayer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        startTime = Time.time;
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + waitTime)
        {
            anim.Play("Bomb_explotion");
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Explotion()//ainimation evnet
    {
        coll.enabled = false;
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        
        rb.gravityScale = 0;
        foreach (var item in aroundObjects)
        {
            Vector3 pos = transform.position - item.transform.position;
            item.GetComponent<Rigidbody2D>().AddForce((-pos+Vector3.up)*bombForce,ForceMode2D.Impulse);
        }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}