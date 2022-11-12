using System;
using UnityEngine;
using PlatformerGameKit;

public class Ability : MonoBehaviour
{
    [SerializeField] protected AttackTransition flyingAnimClip;
    [SerializeField] protected AttackTransition groundAnimClip;

    public Fighter fighter;
    public CharacterController2D controller;
    public PlayerAttackLogic attackLogic;

    //public float baseDamage;

    protected virtual void Start()
    {
        attackLogic = transform.parent.GetComponent<PlayerAttackLogic>(); // TODO: enemies cant use this
        
        if (controller != null)
        {
            flyingAnimClip.Events.OnEnd += SendAbilityEnd;
            groundAnimClip.Events.OnEnd += SendAbilityEnd;
            
            flyingAnimClip.Events.OnEnd += controller.characterStateMachine.ForceSetDefaultState;
            groundAnimClip.Events.OnEnd += controller.characterStateMachine.ForceSetDefaultState;
        }
    }

    public virtual AttackTransition GetAnim()
    {
        if (controller != null)
        {
            return controller.isGrounded ? groundAnimClip : flyingAnimClip;
        }

        return null;
    }

    public void SendAbilityEnd() => attackLogic.AttackDone(this);
}