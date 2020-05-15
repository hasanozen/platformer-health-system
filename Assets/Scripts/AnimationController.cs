using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance { get; private set; }

    private Animator animator;
    public SpriteRenderer spriteRenderer { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetJumpAnim(bool isGrounded)
    {
        animator.SetBool("isGrounded", isGrounded);
    }

    public void SetRunAnim(Rigidbody2D rb)
    {
        if (rb.velocity.x < 0)
            spriteRenderer.flipX = true;
        else if (rb.velocity.x > 0)
            spriteRenderer.flipX = false;

        animator.SetFloat("moveSpeed", Math.Abs(rb.velocity.x));
    }

    public void SetHurtAnim()
    {
        animator.SetTrigger("hurt");
    }
}
