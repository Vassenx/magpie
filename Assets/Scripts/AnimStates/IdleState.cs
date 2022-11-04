using UnityEngine;
using Animancer;

public class IdleState : CharacterBaseState
{
    [SerializeField]  private ClipTransition IdleGround;
    [SerializeField]  private ClipTransition IdleFly;

    private AnimationClip idle;
    [HideInInspector] public ClipTransition Idle
    {
        get { return controller.isGrounded ? IdleGround : IdleFly; }
    }
    
    public override void OnEnterState()
    {
        base.OnEnterState();
        animancer.Play(Idle);
    }

    private void Update()
    {
        animancer.Play(Idle);
    }
}
