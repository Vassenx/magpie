using System.Collections;
using UnityEngine;

namespace Magpie
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerController2D : CharacterController2D
    {
        // Move player in 2D space
        public Vector2 speed = new Vector2(250f, 200f);
        public float maxSpeed = 8;

        private bool isDashing;
        
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
            if (Mathf.Approximately(value.x, 0))
            {
                value.x = 0;
            }
            
            _movementInput = value.normalized;
        }
        
        // Warning: completely takes over velocity
        public IEnumerator Dash(Rigidbody2D r2d, float dashForce, Vector2 direction, float duration)
        {
            isDashing = true;
            r2d.velocity = direction * dashForce;

            yield return new WaitForSeconds(duration);

            r2d.velocity = Vector3.zero;

            isDashing = false;
        }

        protected override void FixedUpdate()
        {
            if (_movementInput.sqrMagnitude > 0.1 && !isDashing)
            {
                r2d.velocity += _movementInput * speed * Time.fixedDeltaTime;
                r2d.velocity = Vector3.ClampMagnitude(r2d.velocity, maxSpeed);
            }

            if (_movementInput.sqrMagnitude < 0.1 && !isDashing && isGrounded)
            {
                r2d.velocity = Vector3.zero;
            }
            
            base.FixedUpdate();
        }
    }
}
