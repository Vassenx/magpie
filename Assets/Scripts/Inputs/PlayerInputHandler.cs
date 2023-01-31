using System.Collections;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Magpie
{

    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private PlayerController2D playerController;
        [SerializeField] private AttackLogic playerAttackLogic;
        [SerializeField] private PlayerFighter fighter;

        [SerializeField] private MovementState movementAnimState;

        public void OnMove(InputValue input)
        {
            Vector2 inputVec = input.Get<Vector2>();
            playerController.SetMovement(inputVec);

            // set anim
            if (!Mathf.Approximately(inputVec.x, 0) || !Mathf.Approximately(inputVec.y, 0))
            {
                movementAnimState.OwnerStateMachine.TrySetState(movementAnimState);
            }
        }

        public void OnMeleeAbility(InputValue input)
        {
            if (input.isPressed)
            {
                playerAttackLogic.OnAttackInput(fighter.curMeleeAbility);
            }
        }

        public void OnRangedAbility(InputValue input)
        {
            if (input.isPressed)
            {
                playerAttackLogic.OnAttackInput(fighter.curRangedAbility);
            }
        }

        public void OnDash(InputValue input)
        {
            if (input.isPressed)
            {
                playerAttackLogic.OnAttackInput(fighter.curDashAbility);
            }
        }
    }
}
