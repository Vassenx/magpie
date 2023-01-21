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
            if (rangedAbility == null)
            {
                rangedAbility = context.enemyFighter.GetComponentInChildren<RangedAbility>(); // TODO: this is terrible, use the projectilesettings & in the BT from Assets/
            }
            if(rangedAbility != null)
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
