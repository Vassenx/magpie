using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Magpie
{
    public class MoveToTargetAction : ActionNode
    {
        [SerializeField] private float tolerance = 1.0f;
        
        protected override void OnStart()
        {
            if (blackboard.curTarget == null)
                return;
            
            context.agent.destination = blackboard.curTarget.position;
        }

        protected override void OnStop()
        {
        }
        
        protected override State OnUpdate()
        {
            if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid || blackboard.curTarget == null)
            {
                context.agent.isStopped = true;
                context.agent.ResetPath();
                return State.Failure;
            }

            if (context.agent.remainingDistance < tolerance)
            {
                return State.Success;
            }
            
            if (context.agent.pathPending)
            {
                return State.Running;
            }

            // go and face target
            context.agent.destination = blackboard.curTarget.position;
            context.enemyFighter.FaceTarget();
            
            return State.Running;
        }
    }
}
