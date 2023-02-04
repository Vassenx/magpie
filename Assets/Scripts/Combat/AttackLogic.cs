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

        [SerializeField] private AbilityState abilityAnimState;

        public void OnAttackInput(Ability ability)
        {
            if (!inAttack)
            {
                abilityAnimState.SetAnim(ability.GetAnim(), ability);
                abilityAnimState.OwnerStateMachine.TrySetState(abilityAnimState);
            }
            else
            {
                // MeleeCombo(attack);
            }
        }

        // TODO: stuff below

        public void AttackStart(Ability ability)
        {
            _prevAttack = ability;

            canCombo = true;
            inAttack = true;
        }
        
        // ANIMATION EVENT
        public void AttackDone(Ability ability)
        {
            if (ability == _prevAttack)
            {
                inAttack = false;
            }
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
