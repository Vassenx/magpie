using System;
using UnityEngine;
using Animancer.FSM;
using Animancer;

public class CharacterBaseState : StateBehaviour, IOwnedState<CharacterBaseState>
{
    public StateMachine<CharacterBaseState> OwnerStateMachine => controller.characterStateMachine;

    protected AnimancerComponent animancer;
    protected Rigidbody2D rb;
    protected CharacterController2D controller;

    public virtual bool CanTurn => true;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
            
        animancer = GetComponent<AnimancerComponent>();
        
        rb = transform.parent.GetComponent<Rigidbody2D>();
        controller = transform.parent.GetComponent<CharacterController2D>();
        
        //gameObject.GetComponentInParentOrChildren(ref _Animancer);
    }
#endif
    
    protected virtual void Awake()
    {
    }

    public override void OnEnterState()
    {
    }
    
    public override void OnExitState()
    {
    }
}