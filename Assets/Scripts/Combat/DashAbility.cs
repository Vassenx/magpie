using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magpie
{
    public class DashAbility : MeleeAbility
    {
        [SerializeField] private float dashForce;
        [SerializeField] private float duration = 0.15f;

        public void Dash() // Anim clip event
        {
            // player only right now
            var playerController = controller as PlayerController2D;
            if (playerController != null)
            {
                Vector2 direction = playerController.facingRight ? fighter.transform.right : -fighter.transform.right;

                StartCoroutine(playerController.Dash(playerController.r2d, dashForce, direction, duration));
            }
        }
    }
}