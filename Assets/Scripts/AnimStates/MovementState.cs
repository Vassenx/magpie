using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Unity.VisualScripting;

public class MovementState : CharacterBaseState
{
    private ClipTransition curAnim;
    public ClipTransition CurAnim
    {
        get { return UpdateCurAnimClip();  }
    }

    [SerializeField] private ClipTransition walk;
    
    [SerializeField] private ClipTransition fly;
    [SerializeField] private ClipTransition fall;
    private ClipTransition idle;
    
    // TODO :        public override bool CanEnterState

    protected override void Awake()
    {
        base.Awake();
        idle = GetComponent<IdleState>().Idle;
    }

    private ClipTransition UpdateCurAnimClip()
    {
        if (rb == null)
            return idle;

        if (controller.isGrounded)
        {
            return Mathf.Approximately(rb.velocity.x, 0) ? idle : walk;
        }
        else
        {
            bool goingUp = rb.velocity.y > 0;
            return goingUp ? fly : fall;
        }
    }
    
    public override void OnEnterState()
    {
        base.OnEnterState();
        animancer.Play(CurAnim);
    }
    
    private void Update()
    {
        animancer.Play(CurAnim);
    }
}
