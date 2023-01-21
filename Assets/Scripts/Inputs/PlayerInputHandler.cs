using Animancer.FSM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Magpie
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerController2D playerController;
        [SerializeField] private AttackInputMappings inputMappings;
        [SerializeField] private AttackLogic playerAttackLogic;

        [SerializeField] private MovementState movementAnimState;

        public void OnMove(InputValue input)
        {
            Vector2 inputVec = input.Get<Vector2>();
            playerController.SetMovement(inputVec);

            // anim
            if (!Mathf.Approximately(inputVec.x, 0) || !Mathf.Approximately(inputVec.y, 0))
            {
                movementAnimState.OwnerStateMachine.TrySetState(movementAnimState);
            }
        }

        public void OnMeleeAbility(InputValue input)
        {
            if (input.isPressed)
            {
                MeleeAbility meleeAbility =
                    (MeleeAbility)inputMappings.GetCurAssociatedAttack(AbilityInputNamesEnum.MeleeAbility); //TODO, not just dash
                playerAttackLogic.OnMeleeInput(meleeAbility);
            }
        }

        public void OnRangedAbility(InputValue input)
        {
            if (input.isPressed)
            {
                RangedAbility rangedAbility =
                    (RangedAbility)inputMappings.GetCurAssociatedAttack(AbilityInputNamesEnum.RangedAbility);
                playerAttackLogic.OnRangedInput(rangedAbility);
            }
        }

        public void OnDash(InputValue input)
        {
            if (input.isPressed)
            {
                MeleeAbility dashAbility =
                    (MeleeAbility)inputMappings.GetCurAssociatedAttack(AbilityInputNamesEnum.DashAbility);
                playerAttackLogic.OnMeleeInput(dashAbility);
            }
        }
    }
}
