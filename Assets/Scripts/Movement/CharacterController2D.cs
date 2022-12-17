using System;
using UnityEngine;
using Animancer.FSM;
using UnityEngine.AI;

namespace Magpie
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class CharacterController2D : MonoBehaviour
    {
        public readonly StateMachine<CharacterBaseState>.WithDefault characterStateMachine =
            new StateMachine<CharacterBaseState>.WithDefault();

        public Rigidbody2D r2d { get; private set; }
        public NavMeshAgent aiAgent { get; private set; }
        private CapsuleCollider2D mainCollider;
        [SerializeField] private SpriteRenderer sprite;

        public static event Action<bool> OnGroundedChanged;

        public static readonly float GRAVITY_SCALE = 0.5f; // static for now
        public bool isGrounded { get; protected set; }
        public bool facingRight { get; protected set; }


        protected virtual void Awake()
        {
            characterStateMachine.DefaultState = GetComponentInChildren<IdleState>();
            r2d = GetComponent<Rigidbody2D>();
            aiAgent = GetComponent<NavMeshAgent>();
            mainCollider = GetComponent<CapsuleCollider2D>();
            isGrounded = true;
        }

        protected virtual void Start()
        {
            r2d.gravityScale = GRAVITY_SCALE;
            facingRight = transform.localScale.x > 0;
        }

        protected virtual void FixedUpdate()
        {
            UpdateIsGrounded();
            FlipFacing();
        }

        protected virtual void FlipFacing()
        {
            if (!characterStateMachine.CurrentState.CanTurn)
                return;

            // Change facing direction
            float xVel = aiAgent == null ? r2d.velocity.x : aiAgent.velocity.x; // rigidbody velocity not correct for ai agents in Unity

            if ((xVel > 0 && !facingRight) || (xVel < 0 && facingRight))
            {
                facingRight = !facingRight;
                Transform transf = sprite.transform;
                transf.RotateAround(transf.position, transf.up, 180f);
                //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

        protected void UpdateIsGrounded()
        {
            bool wasGrounded = isGrounded;
            isGrounded = false;

            Bounds colliderBounds = mainCollider.bounds;
            float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
            Vector3 groundCheckPos =
                colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
            //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i] != mainCollider)
                    {
                        isGrounded = true;
                        break;
                    }
                }
            }

            if (wasGrounded != isGrounded)
            {
                OnGroundedChanged?.Invoke(isGrounded);
            }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0),
                isGrounded ? Color.green : Color.red);
            Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0),
                isGrounded ? Color.green : Color.red);
#endif
        }
    }
}
