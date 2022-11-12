using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magpie
{
    public class ProjectileHitBox : IHitBox
    {
        protected Projectile projectile;

        public void Activate(Projectile proj, Fighter owner, float damage, List<Fighter> ignore = null)
        {
            projectile = proj;
            base.Activate(owner, damage, ignore);
        }

        protected override void OnValidHit(Fighter target, float damage)
        {
            if (!projectile.isActiveAndEnabled)
                return;

            base.OnValidHit(target, damage);

            projectile.Impact();
        }

        protected override void OnInvalidHit(Collider2D col)
        {
            if (!projectile.isActiveAndEnabled)
                return;

            if (col.gameObject == ownerFighter.gameObject)
                return;

            base.OnInvalidHit(col);

            PoolsManager.projPool.RecycleObject(projectile);
        }
    }
}
