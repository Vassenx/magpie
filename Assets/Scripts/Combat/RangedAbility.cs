using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magpie
{
    [System.Serializable]
    public class RangedAbility : Ability
    {
        [SerializeField] private ProjectileSettings projectileSettings;

        public void ShootProjectile() // Anim clip event
        {
            Projectile projectile = PoolsManager.projPool.ObtainObject(null);

            projectile.Fire(fighter, projectileSettings);
        }
    }
}
