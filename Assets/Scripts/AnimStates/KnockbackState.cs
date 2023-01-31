using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animancer;
using Animancer.FSM;
using PlatformerGameKit;
using UnityEngine;

namespace Magpie
{
    public class KnockbackState : CharacterBaseState
    {
        [SerializeField] private ClipTransition Knockback;

        public override bool CanTurn => false;
        public override bool CanExitState => false;

        protected override void Awake()
        {
            base.Awake();            
            Knockback.Events.OnEnd += controller.characterStateMachine.ForceSetDefaultState;
        }

        private void PlayAnimation()
        {
            animancer.Play(Knockback);
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            PlayAnimation();
        }
    }
}