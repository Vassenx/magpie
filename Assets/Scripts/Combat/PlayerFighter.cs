using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Magpie
{
    public class PlayerFighter : Fighter
    {
        [Header("Abilities")]
        [ValueDropdown("GetAbilities")][SerializeField] private MeleeAbility _curMeleeAbility;
        [ValueDropdown("GetAbilities")][SerializeField] private RangedAbility _curRangedAbility;
        [ValueDropdown("GetAbilities")][SerializeField] private DashAbility _curDashAbility;
        
        public MeleeAbility curMeleeAbility { get { return _curMeleeAbility;  } }
        public RangedAbility curRangedAbility { get { return _curRangedAbility;  } }
        public DashAbility curDashAbility { get { return _curDashAbility;  } }

        protected override void KillEntity()
        {
            base.KillEntity();

            if (controller)
            {
                controller.enabled = false;
            }
        }
        
        private IEnumerable GetAbilities()
        {
            return GetComponentsInChildren<Ability>()
                .Select(x => new ValueDropdownItem(x.abilityName, x));
        }
    }
}
