using UnityEngine;
using Animancer;

namespace Magpie
{
    public class LandState : CharacterBaseState
    {
        [SerializeField] private ClipTransition Land;

        protected override void Awake()
        {
            base.Awake();

            Land.Events.OnEnd += controller.characterStateMachine.ForceSetDefaultState;

            CharacterController2D.OnGroundedChanged += (bool isGrounded) =>
            {
                if (isGrounded)
                {
                    controller.characterStateMachine.TrySetState(this);
                }
            };
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            animancer.Play(Land);
        }
    }
}
