using UnityEngine;
using Animancer;

namespace Magpie
{
    public class TakeOffState : CharacterBaseState
    {
        [SerializeField] private ClipTransition TakeOff;

        protected override void Awake()
        {
            base.Awake();

            controller.OnGroundedChanged += (bool isGrounded) =>
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
}
