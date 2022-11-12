using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Magpie
{
    [System.Serializable]
    public class ShootAction : ActionNode
    {
        [SerializeField] private RangedAbility rangedAbility;
        
        protected override void OnStart()
        {
            context.aiController.attackLogic.OnRangedInput(rangedAbility);
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
