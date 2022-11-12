using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

namespace Magpie
{
    [RequireComponent(typeof(Collider2D))]
    public class IHitBox : MonoBehaviour // yeah yeah its not an interface
    {
        [Header("Component Refs")] [SerializeField]
        protected Collider2D trigger;

        protected Fighter ownerFighter;

        [Header("Hit Settings")] protected float baseDamage; // TODO: struct if need more vars
        [SerializeField] protected bool CanHitSameTargetAgain = false;
        [SerializeField] protected List<Fighter> fightersToIgnore;

        protected void Awake()
        {
            trigger.isTrigger = true;
            fightersToIgnore ??= new List<Fighter>();
        }

        public virtual void Activate(Fighter owner, float damage, List<Fighter> ignore = null)
        {
            ownerFighter = owner;
            baseDamage = damage;
            if (ignore != null)
            {
                fightersToIgnore.AddRange(ignore);
            }
        }

        protected void OnTriggerEnter2D(Collider2D col)
        {
            Fighter target = GetTarget(col);
            if (CheckForValidHit(target))
            {
                OnValidHit(target, baseDamage);
            }

            OnInvalidHit(col);
        }

        protected virtual bool CheckForValidHit(Fighter target)
        {
            if (target == null || target == ownerFighter)
                return false;

            if (fightersToIgnore != null && fightersToIgnore.Contains(target)) // !target.CanBeHit(ref this)
                return false;

            return true;
        }

        protected virtual void OnValidHit(Fighter target, float damage)
        {
            if (!CanHitSameTargetAgain)
                fightersToIgnore?.Add(target);

            target.OnTakeDamage(damage);
        }

        protected virtual void OnInvalidHit(Collider2D col)
        {
        }

        private void OnDisable()
        {
            fightersToIgnore.Clear(); // TODO: cant do this as some are added in the inspector/editormode
        }

        public static Fighter GetTarget(Component component)
            => GetTarget(component.gameObject);

        public static Fighter GetTarget(GameObject gameObject)
            => gameObject.transform.parent == null
                ? gameObject.GetComponent<Fighter>()
                : gameObject.GetComponentInParent<Fighter>();
    }
}
