using System;
using System.Collections;
using System.Collections.Generic;
using PlatformerGameKit;
using UnityEngine;
using UnityEngine.AI;

namespace Magpie
{
    public partial class Fighter : MonoBehaviour
    {
        [Header("Fighter Stats Info")] [SerializeField]
        protected FighterStats stats;

        [SerializeField] protected bool isHittable = true;
        [SerializeField] protected Animator animator;
        [SerializeField] protected SpriteRenderer sprite;
        public CharacterController2D controller { get; protected set; }
        public Transform curTarget; // TODO

        [HideInInspector] public float curHealth { get; protected set; }
        [HideInInspector] public bool inCombat { get; protected set; } // TODO: change to false (player vs enemy
        [HideInInspector] public float lastHitTime { get; protected set; }

        public delegate void OnHealthChange(Fighter fighter, float newHealth, float totalHealth);

        public static OnHealthChange HealthChangeEvent;

        protected virtual void Awake()
        {
            inCombat = false;
            isHittable = true;
            curHealth = stats.maxHealth;

            if (!animator)
            {
                animator = GetComponentInChildren<Animator>();
            }

            if (!sprite)
            {
                sprite = GetComponentInChildren<SpriteRenderer>();
            }

            if (!controller)
            {
                controller = GetComponent<CharacterController2D>();
            }
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
            EndHitSequence(); // Hit box
        }

        public virtual void OnTakeDamage(HitData hitInfo) => OnTakeDamage(hitInfo.Damage);

        public virtual void OnTakeDamage(float damage)
        {
            float damageAmount = damage;

            if (!isHittable)
                return;

            if (curHealth <= 0)
            {
                KillEntity();
                return;
            }

            inCombat = true;
            lastHitTime = Time.time;

            curHealth -= damageAmount;
            Mathf.Clamp(curHealth, 0, stats.maxHealth);
            HealthChangeEvent.Invoke(this, curHealth, stats.maxHealth);

            if (animator)
            {
                animator.SetTrigger("Hurt");
            }
        }

        public virtual void GainHealth(int healthBonus)
        {
            curHealth += healthBonus;
            Mathf.Clamp(curHealth, 0, stats.maxHealth);
        }

        protected virtual void KillEntity()
        {
            inCombat = false;
            isHittable = false; //also prevents repeatedly calling this func

            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            if (agent)
            {
                agent.enabled = false;
            }

            if (animator)
            {
                animator.SetTrigger("Die");
            }
        }
    }
}
