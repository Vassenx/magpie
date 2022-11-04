using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animancer;
using Animancer.FSM;
using PlatformerGameKit;
using UnityEngine;

public class AttackBaseState : CharacterBaseState
{
    protected AttackTransition curAttackAnim;
    protected Fighter fighter;

    public override bool CanTurn => false;
    public override bool CanExitState => false;
    
    protected override void Awake()
    {
        base.Awake();
        fighter = GetComponentInParent<Fighter>();
    }
    
    public void SetAnim(AttackTransition anim)
    {
        curAttackAnim = anim; 
    }
    
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
    }
#endif
    
    private void PlayAnimation()
    { 
        fighter.EndHitSequence();
        // var animation = CurrentAnimations[_CurrentIndex];
        //Character.Animancer.Play(GetComponent<Animation>());
        animancer.Play(curAttackAnim);
    }
    
    public override void OnEnterState()
    {
        base.OnEnterState();
        // _CurrentIndex = 0;
        //_Combo = false;
        PlayAnimation();
    }
    
    public override void OnExitState()
    {
        base.OnExitState();
        fighter.EndHitSequence();
    }
}
