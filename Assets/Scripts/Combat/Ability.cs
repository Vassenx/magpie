using System;
using UnityEngine;
using PlatformerGameKit;

public class Ability : MonoBehaviour
{
    public AttackTransition flyingAnimClip;
    public AttackTransition groundAnimClip;

    public CharacterController2D controller;

    //public float baseDamage;

    private void Start()
    {
        controller = transform.parent.GetComponent<CharacterController2D>();
        if (controller != null)
        {
            flyingAnimClip.Events.OnEnd += controller.characterStateMachine.ForceSetDefaultState;
            groundAnimClip.Events.OnEnd += controller.characterStateMachine.ForceSetDefaultState;
        }
    }
}