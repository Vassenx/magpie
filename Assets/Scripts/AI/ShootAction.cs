using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Magpie
{
    [System.Serializable]
    public class ShootAction : ActionNode
    {
        protected override void OnStart()
        {
            //context.aiController.attackLogic;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
    }
}
