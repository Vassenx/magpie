using UnityEngine;

namespace Magpie
{
    public class Projectile : MonoBehaviour
    {
        [Header("Component Refs")] [SerializeField]
        private ProjectileHitBox hitbox;

        [SerializeField] private Rigidbody2D rb;
        private Fighter ownerFighter;

        private ProjectileSettings settings;

        private Vector2 startPos;
        private Vector2 startRot; // TODO
        private float startTime;
        Vector2 direction;

        public void Fire(Fighter owner, ProjectileSettings projSettings)
        {
            ownerFighter = owner;
            settings = projSettings;
            transform.position = owner.transform.position; // TODO parm
            startPos = transform.position;
            startTime = Time.time;
            direction = Vector2.right * (ownerFighter.controller.facingRight ? 1 : -1);

            hitbox.Activate(this, ownerFighter, settings.baseDamage);
        }

        private void FixedUpdate()
        {
            UpdateVelocity();

            Vector2 curPos = transform.position;

            if (Vector2.SqrMagnitude(startPos - curPos) >= settings.maxDistanceSqr
                || Time.time - startTime >= settings.maxLifeTimeSec)
            {
                PoolsManager.projPool.RecycleObject(this);
            }
        }

        private void UpdateVelocity()
        {
            Vector2 baseVelocity = direction * (settings.speed * Time.fixedDeltaTime);
            //rb.MovePosition(transform.position + baseVelocity);
            rb.velocity = new Vector2(baseVelocity.x * Mathf.Cos(settings.angle),
                baseVelocity.y * Mathf.Sin(settings.angle));
        }

        public void Impact()
        {
            if (settings.impactAnimation == null)
            {
                PoolsManager.projPool.RecycleObject(this);
            }
            else
            {
                rb.simulated = false;
                settings.impactAnimation.enabled = true;
                var timeTilDestroy = settings.impactAnimation.Clip.length / settings.impactAnimation.Speed;
                PoolsManager.projPool.RecycleObject(this, timeTilDestroy);
            }
        }
    }
}
