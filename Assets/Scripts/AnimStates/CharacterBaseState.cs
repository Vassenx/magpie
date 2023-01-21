using System;
using UnityEngine;
using Animancer.FSM;
using Animancer;

namespace Magpie
{
    public class CharacterBaseState : StateBehaviour, IOwnedState<CharacterBaseState>
    {
        public StateMachine<CharacterBaseState> OwnerStateMachine => controller.characterStateMachine;

        protected AnimancerComponent animancer;
        protected Rigidbody2D rb;
        protected CharacterController2D controller;

        public virtual PriorityLevel Priority => PriorityLevel.med;// All states default to 0 unless they override it.

        public enum PriorityLevel
        {
            low,
            med,
            high
        };
        
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

        public override bool CanExitState
        {
            get
            {
                var nextState = this.GetNextState();
                return nextState.Priority >= Priority;
            }
        }
        
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
}