using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Magpie
{
    public class AIController : MonoBehaviour
    {
        public AttackLogic attackLogic;

        void Awake()
        {
            var agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }
}
