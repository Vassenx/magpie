using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TheKiwiCoder;

namespace Magpie
{
    [System.Serializable]
    public class ShootAction : ActionNode
    {
        [SerializeField] private string abilityName;
        private Ability ability; // found through abilityName
        
        public override void OnValidateNode()
        {
            if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                return;

            if (abilityName != String.Empty && !description.Contains(abilityName))
            {
                description += abilityName;
            }
        }
        
        protected override void OnStart()
        {
            if (ability == null)
            {
                ability = context.enemyFighter.availableAbilties.FirstOrDefault(x => x.abilityName == abilityName);
            }
            if(ability != null)
                context.aiController.attackLogic.OnAttackInput(ability);
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
