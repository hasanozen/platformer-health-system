using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D playerRB;

    private bool isGrounded;
    private bool canDoubleJump;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private LayerMask layerOfGround;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float knockBackLenght, knockBackForce;

    private float knockBackCounter;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        playerTransform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter <= 0)
        {
            playerRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), playerRB.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, layerOfGround);

            if (isGrounded)
                canDoubleJump = true;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (isGrounded)
                    JumpPlayer();
                else
                {
                    if (canDoubleJump)
                    {
                        JumpPlayer();
                        canDoubleJump = false;
                    }
                }
            }

            AnimationController.Instance.SetRunAnim(playerRB);
            AnimationController.Instance.SetJumpAnim(isGrounded);
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }
    }

    private void JumpPlayer()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLenght;
        playerRB.velocity = new Vector2(0f, knockBackForce);
        playerRB.velocity = AnimationController.Instance.spriteRenderer.flipX == true ?
                new Vector2(knockBackForce, playerRB.velocity.y) : new Vector2(-knockBackForce, playerRB.velocity.y);

        AnimationController.Instance.SetHurtAnim();
    }
}
