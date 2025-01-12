using System;
using UnityEngine;
using Animancer;
using Unity.VisualScripting;

namespace Magpie
{
    public class IdleState : CharacterBaseState
    {
        [SerializeField] private ClipTransition IdleGround;
        [SerializeField] private ClipTransition IdleFly;

        private AnimationClip idle;

        [HideInInspector]
        public ClipTransition Idle
        {
            get { return controller.isGrounded ? IdleGround : IdleFly; }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            animancer.Play(Idle);
        }
    }
}
