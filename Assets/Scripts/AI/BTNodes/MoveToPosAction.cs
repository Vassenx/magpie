using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Magpie
{
    public class MoveToPosAction : ActionNode
    {
        [SerializeField] private float tolerance = 1.0f;

        protected override void OnStart()
        {
            context.agent.destination = blackboard.moveToPosition;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (context.agent.pathPending)
            {
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