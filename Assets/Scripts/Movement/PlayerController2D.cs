using System;
using UnityEngine;
using Animancer.FSM;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

// base code from https://sharpcoderblog.com/blog/2d-platformer-character-controller

public class PlayerController2D : CharacterController2D
{
    // Move player in 2D space
    public Vector2 speed = new Vector2(250f, 200f);

    private bool facingRight = true;

    private float _moveDirection = 0;
    public Vector2 _movementInput { get; private set; }
    

    protected override void Awake()
    {
        base.Awake();
        isGrounded = false; //overridden
    }

    protected override void Start()
    {
        base.Start();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
        facingRight = transform.localScale.x > 0;
    }

    protected override void Update()
    {
        base.Update();
        FlipFacing();
    }

    public void SetMovement(Vector2 value)
    {
         _movementInput = value.normalized;
         
        if (Mathf.Approximately(value.x, 0))
        {
            value.x = 0;
        }
        _moveDirection = value.x;
    }

    protected override void FlipFacing()
    {
        base.FlipFacing();
        
        // Change facing direction
        if (_moveDirection != 0)
        {
            if (_moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (_moveDirection < 0 && facingRight)
            {
                facingRight = false;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        } 
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        r2d.velocity = _movementInput * speed * Time.fixedDeltaTime;
    }
}
