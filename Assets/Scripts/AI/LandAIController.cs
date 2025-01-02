using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Magpie
{
    public class LandAIController : MonoBehaviour
    {
        [FormerlySerializedAs("rayCastCheckDistanceDown")] [SerializeField] private float rayCastCheckDistanceAbove = 0.5f;
        [FormerlySerializedAs("rayCastCheckDistanceForward")] [SerializeField] private float rayCastCheckDistanceBelow = 1f;

        [SerializeField] private LayerMask groundLayerMask;
        private static readonly string GroundLayerName = "Ground";
        private RaycastHit2D hitAbove;
        private RaycastHit2D hitBelow;
        private Vector2 rayCastDirectionAbove;
        private Vector2 rayCastDirectionBelow;
        [SerializeField] private Rigidbody2D rb;
        private Vector2 startPos;
        [SerializeField] private float rotSpeed = 5f;
        private bool isRotating = false;
        [SerializeField] private float speed = 1f;
        private Coroutine rotateCoroutine = null;
        void Start()
        {
            groundLayerMask = 1 << LayerMask.NameToLayer(GroundLayerName);
            rb = GetComponent<Rigidbody2D>();
            startPos = transform.position;
            rb.velocity = transform.right * speed;
        }

        void Update()
        {
            Vector2 up = transform.up;
            rayCastDirectionBelow = (rb.velocity + -1*up).normalized;
            rayCastDirectionAbove = (rb.velocity + up).normalized;
            
            hitBelow = Physics2D.Raycast(transform.position, rayCastDirectionBelow, rayCastCheckDistanceBelow, groundLayerMask);
            hitAbove = Physics2D.Raycast(transform.position, rayCastDirectionAbove, rayCastCheckDistanceAbove, groundLayerMask);

            if (hitBelow.collider == null && hitBelow.collider == null)
            {
                //transform.position = startPos;
                Debug.LogWarning(name + " off path, resetting to start pos");
                //return;
            }

            if (hitAbove.collider == null && hitBelow.collider == null && !isRotating) // curve around convex ground
            {
                rotateCoroutine = StartCoroutine(RotateUntil(Time.time, true));
                isRotating = true;
                return;
            }
            else if (hitAbove.collider != null && hitBelow.collider != null && !isRotating) // curve around concave ground
            {
                rotateCoroutine = StartCoroutine(RotateUntil(Time.time, false));
                isRotating = true;
                return;
            }
            else if(hitAbove.collider == null && hitBelow.collider != null && isRotating)
            {
                if (rotateCoroutine != null)
                {                
                    StopCoroutine(rotateCoroutine);
                    rotateCoroutine = null;
                }
                isRotating = false;
                return;
            }

            rb.velocity = transform.right * speed;
        }
            
        IEnumerator RotateUntil (float startTime, bool curveBelow) 
        { 
           // Quaternion forwardRot = Quaternion.FromToRotation (Vector3.up, rb.velocity.normalized);

           var newRot = transform.rotation; //Quaternion.LookRotation( transform.right, transform.up);
           float angle = curveBelow ? -90 : 90;
           newRot *= Quaternion.Euler(0, 0, angle); 
            float t = 0f;

            yield return null;
            
            while (t < 1f)
            {
                t = 0.1f * (Time.time - startTime);
                rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, newRot, t);
                yield return null;
            }
        }
        
        void OnDrawGizmosSelected()
        {
            Gizmos.color = hitBelow.collider == null ? Color.red : Color.green;
            Gizmos.DrawRay(transform.position, rayCastDirectionBelow * rayCastCheckDistanceBelow);
            Gizmos.color = hitAbove.collider == null ? Color.red : Color.green;
            Gizmos.DrawRay(transform.position, rayCastDirectionAbove * rayCastCheckDistanceAbove);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, transform.right * rayCastCheckDistanceAbove);
        }
    }
}
