using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using PlatformerGameKit;
using UnityEngine;

public class PlayerAttackLogic : MonoBehaviour
{
    [SerializeField] private PlayerFighter playerFighter;

    private bool canCombo = true;
    private bool inAttack = false;
    private MeleeAbility _prevMeleeAttack;
    
    private LayerMask enemyLayerMask; // TODO not in use, add to hitbox stuff, also its not specific to player
    private ContactFilter2D enemyContactFilter; // TODO not in use, add to hitbox stuff, also its not specific to player

    [SerializeField] private AbilityState abilityAnimState;
    
    private void Start()
    {
        enemyLayerMask = LayerMask.NameToLayer("Enemy");
        
        enemyContactFilter = new ContactFilter2D();
        enemyContactFilter.SetLayerMask(enemyLayerMask);
    }

    public void OnMeleeInput(MeleeAbility meleeAbility)
    {
        if (!inAttack)
        {
            _prevMeleeAttack = meleeAbility; // TODO, below try set state?

            canCombo = true;
            inAttack = true;

            abilityAnimState.SetAnim(meleeAbility.flyingAnimClip);
            abilityAnimState.OwnerStateMachine.TrySetState(abilityAnimState);
        }
        else
        {
            // MeleeCombo(attack);
        }
    }

    public void OnRangedInput(RangedAbility rangedAbility)
    {
        // TODO
    }

    // ANIMATION EVENT
    public void AttackDone(MeleeAbility meleeAttack)
    {
        if (meleeAttack == _prevMeleeAttack)
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
