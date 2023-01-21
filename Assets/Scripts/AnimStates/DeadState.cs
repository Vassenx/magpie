using UnityEngine;
using Animancer;

namespace Magpie
{
    public class DeadState : CharacterBaseState
    {
        [SerializeField] private ClipTransition Die;
        [SerializeField] private ClipTransition DeadLoop;

        public override PriorityLevel Priority => PriorityLevel.high;

        public override bool CanTurn => false;

        protected override void Awake()
        {
            base.Awake();

            Die.Events.OnEnd += LoopDeath; // TODO Im pretty sure theres a multi-anim component thing
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            animancer.Play(Die);
        }

        private void LoopDeath()
        {
            animancer.Play(DeadLoop);
        }
    }
}