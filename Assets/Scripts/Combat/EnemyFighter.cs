using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Sirenix.OdinInspector;
using TheKiwiCoder;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Magpie
{
    public class EnemyFighter : Fighter
    {
        [Header("Aggro")] 
        [SerializeField] private float radius = 10f;
        [SerializeField] private float fovAngleDeg = 120f;
        [SerializeField] private LayerMask aggroLayerMask;
        [ValueDropdown("GetAbilities")][SerializeField] private Ability rangedAbility;

        [SerializeField] private BehaviourTreeRunner btreeRunner;
        
        private void Update() // fixedupdate?
        {
            Transform player = FindPlayer();
            curTarget = player;
            if (btreeRunner != null)
            {
                btreeRunner.tree.blackboard.curTarget = curTarget;
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
        
                
        private IEnumerable GetAbilities()
        {
            return GetComponentsInChildren<Ability>()
                .Select(x => new ValueDropdownItem(x.abilityName, x));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}