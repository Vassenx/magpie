using UnityEngine;
using Animancer;

public class TakeOffState : CharacterBaseState
{
    [SerializeField] private ClipTransition TakeOff;
    
    protected override void Awake()
    {
        base.Awake();
        
        CharacterController2D.OnGroundedChanged += (bool isGrounded) =>
        {
            if (!isGrounded)
            {
                controller.characterStateMachine.TrySetState(this);
            }
        };
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
        animancer.Play(TakeOff);
    }
}
