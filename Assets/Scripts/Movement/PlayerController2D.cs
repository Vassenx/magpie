using UnityEngine;

// base code from https://sharpcoderblog.com/blog/2d-platformer-character-controller

namespace Magpie
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerController2D : CharacterController2D
    {
        // Move player in 2D space
        public Vector2 speed = new Vector2(250f, 200f);

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

        protected override void FixedUpdate()
        {
            r2d.velocity = _movementInput * speed * Time.fixedDeltaTime;

            base.FixedUpdate();
        }
    }
}
