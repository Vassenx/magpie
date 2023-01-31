using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using PlatformerGameKit;
using UnityEngine;

namespace Magpie
{
    public class AttackLogic : MonoBehaviour
    {
        private bool canCombo = true;
        private bool inAttack = false;
        private Ability _prevAttack;

        private LayerMask enemyLayerMask; // TODO not in use, add to hitbox stuff, also its not specific to player

        private ContactFilter2D
            enemyContactFilter; // TODO not in use, add to hitbox stuff, also its not specific to player

        [SerializeField] private AbilityState abilityAnimState;

        private void Start()
        {
            enemyLayerMask = LayerMask.NameToLayer("Enemy");

            enemyContactFilter = new ContactFilter2D();
            enemyContactFilter.SetLayerMask(enemyLayerMask);
        }

        public void OnAttackInput(Ability ability)
        {
            if (!inAttack)
            {
                _prevAttack = ability; // TODO, below try set state?

                canCombo = true;
                inAttack = true;

                abilityAnimState.SetAnim(ability.GetAnim());
                abilityAnimState.OwnerStateMachine.TrySetState(abilityAnimState);
            }
            else
            {
                // MeleeCombo(attack);
            }
        }

        // TODO: stuff below

        // ANIMATION EVENT
        public void AttackDone(Ability ability)
        {
            if (ability == _prevAttack)
            {
                inAttack = false;
            }

            inAttack = false; // temp, figuring out range combo stuff
        }

        // TODO: combo stuff
        /*public void MeleeCombo(Attack attack2)
        {
            //if (playerInputHandle.flagCombo)
            {
                canCombo = false;
                animator.SetBool("CanCombo", canCombo);
                if (prevAttack.Equals(attack2)
                {
                    animator.SetTrigger(attack2.AnimName);
                    prevAttack = attack2;
                }
            }
        }*/
    }
}
