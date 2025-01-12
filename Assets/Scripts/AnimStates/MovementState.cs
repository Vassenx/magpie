using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using Unity.VisualScripting;

namespace Magpie
{
    public class MovementState : CharacterBaseState
    {
        private ClipTransition curAnim;

        public ClipTransition CurAnim
        {
            get { return UpdateCurAnimClip(); }
        }

        [SerializeField] private ClipTransition walk;

        [SerializeField] private ClipTransition fly;
        [SerializeField] private ClipTransition fall;
        [SerializeField] private ClipTransition idleGround;
        
        // TODO :        public override bool CanEnterState

        private ClipTransition UpdateCurAnimClip()
        {
            if (rb == null)
                return GetComponent<IdleState>().Idle;

            if (controller.isGrounded)
            {
                return Mathf.Approximately(rb.velocity.x, 0) ? idleGround : walk;
            }
            else
            {
                bool goingUp = rb.velocity.y > 0;
                return goingUp ? fly : fall;
            }
        }

        public override void OnEnterState()
        {
            Debug.Log("begin");

            base.OnEnterState();
            animancer.Play(CurAnim);
        }
        
        public override void OnExitState()
        {
            Debug.Log("exit");
            base.OnExitState();
        }
        
        private void Update()
        {
            animancer.Play(CurAnim);
        }
    }
}
