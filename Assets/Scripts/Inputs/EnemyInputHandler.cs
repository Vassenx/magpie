using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Magpie
{
    public class EnemyInputHandler : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private MovementState movementAnimState;

        private void Update()
        {
            var agentVel = agent.velocity;
            if (!Mathf.Approximately(agentVel.x, 0) || !Mathf.Approximately(agentVel.y, 0))
            {
                OnMove();
            }
        }

        private void OnMove()
        {
            movementAnimState.OwnerStateMachine.TrySetState(movementAnimState);
        }
    }
}