using System;
using UnityEngine;
using PlatformerGameKit;

namespace Magpie
{
    [System.Serializable]
    public class Ability : MonoBehaviour
    {
        public string abilityName;

        [SerializeField] protected AttackTransition flyingAnimClip;
        [SerializeField] protected AttackTransition groundAnimClip;

        [SerializeField] protected Fighter fighter;
        [SerializeField] protected CharacterController2D controller;
        [SerializeField] protected AttackLogic attackLogic;

        //public float baseDamage;

        protected virtual void Start()
        {
            attackLogic = transform.parent.GetComponent<AttackLogic>();

            if (controller != null)
            {
                flyingAnimClip.Events.OnEnd += SendAbilityEnd;
                groundAnimClip.Events.OnEnd += SendAbilityEnd;

                flyingAnimClip.Events.OnEnd += controller.characterStateMachine.ForceSetDefaultState;
                groundAnimClip.Events.OnEnd += controller.characterStateMachine.ForceSetDefaultState;
            }
        }

        public virtual AttackTransition GetAnim()
        {
            if (controller != null)
            {
                return controller.isGrounded ? groundAnimClip : flyingAnimClip;
            }

            return null;
        }

        public void SendAbilityEnd() => attackLogic.AttackDone(this);
    }
}