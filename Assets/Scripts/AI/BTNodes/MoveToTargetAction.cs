using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Magpie
{
    public class MoveToTargetAction : ActionNode
    {
        [SerializeField] private float tolerance = 1.0f;
        [SerializeField] private float deAggroTimer = 5f; // time until give up trying when target null
        private float startTime;
        
        protected override void OnStart()
        {
            if (blackboard.curTarget == null)
                return;
            
            context.agent.destination = blackboard.curTarget.position;
            startTime = Time.time;
        }

        protected override void OnStop()
        {
        }
        
        protected override State OnUpdate()
        {
            if (!context.agent.pathPending && blackboard.curTarget == null)
            {
                context.agent.isStopped = true;
                context.agent.ResetPath();
                return State.Failure;
            }
            
            if (context.agent.pathPending)
            {
                if (blackboard.curTarget == null)
                {
                    if ((Time.time - startTime) > deAggroTimer)
                        return State.Failure;
                }
                else
                {
                    context.agent.destination = blackboard.curTarget.position; // TODO error here
                    context.enemyFighter.FaceTarget();
                }
                
                return State.Running;
            }

            if (context.agent.remainingDistance < tolerance)
            {
                return State.Success;
            }

            if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
            {
                return State.Failure;
            }

            return State.Running;
        }
    }
}
