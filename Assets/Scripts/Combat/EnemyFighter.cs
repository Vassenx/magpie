using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TheKiwiCoder;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Magpie
{
    [RequireComponent(typeof(BehaviourTreeRunner))]
    public class EnemyFighter : Fighter
    {
        [Header("Aggro")] 
        [SerializeField] private float radius = 10f;
        [SerializeField] private float fovAngleDeg = 120f;
        [SerializeField] private LayerMask aggroLayerMask;
        [SerializeField] private float deAggroTimer = 5f; // time until give up trying when target null
        private float startAggroTime;
        
        [Header("AI")]
        [SerializeField] private BehaviourTreeRunner btreeRunner;
        
        [Header("Abilities")]
        [SerializeField] private Ability[] _availableAbilities;
        public Ability[] availableAbilties { get { return _availableAbilities; } }

        private void Start()
        {
            if (_availableAbilities.IsNullOrEmpty())
            {
                _availableAbilities = GetComponentsInChildren<Ability>();
            }
        }
        
        private void Update() // fixedupdate?
        {
            if (curTarget == null)
            {
                TryAggro();  
            }
            else
            {
                TryDeAggro();
            }
        }

        private void TryAggro()
        {
            curTarget = FindPlayer();
            btreeRunner.tree.blackboard.curTarget = curTarget;

            if (curTarget != null)
            {
                startAggroTime = Time.time;
            }
        }
        
        public void TryDeAggro()
        {
            if ((Time.time - startAggroTime) > deAggroTimer)
            {
                curTarget = null;
                btreeRunner.tree.blackboard.curTarget = null;
            }
        }

        private Transform FindPlayer()
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, radius, aggroLayerMask);
            if (col != null && col.CompareTag("Player"))
            {
                // in view
                if (Vector2.Angle(transform.position, (col.transform.position)) < fovAngleDeg)
                {
                    return col.transform;
                }
            }

            return null;
        }

        public void FaceTarget()
        {
            Vector3 direction = (curTarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}