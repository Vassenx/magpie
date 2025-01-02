using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Magpie
{
    // TODO: animation set up, hide health bar on die, title pop up, etc
    public class FlipWorldMontage : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRb;
        [SerializeField] private GameObject[] vCams = new GameObject[2];
        [SerializeField] private PlayerController2D controller;
        [SerializeField] private DeadState deadAnimState;
        [SerializeField] private TextMeshPro titleText;

        [SerializeField] private PolygonCollider2D triggerCollider;
        
        public float turnSpeed = 5;
        public float gravityFallSpeed = 25f;
        
        private bool shouldRotate = false;
        private float lerpT;

        private void Awake()
        {
            lerpT = 0;
            titleText.enabled = false;
        }

        // when hit telephone pole
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                playerRb.gameObject.GetComponent<PlayerInputHandler>().ToggleAllInputs(false);
                StartCoroutine(WaitThenRotate(3));
            }
        }

        private void RotateToHell()
        {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            
            vCams[1].SetActive(true);
            vCams[0].SetActive(false);
            Debug.Log("start rot");
            shouldRotate = true;
            
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionX;
            
            controller.OnGroundedChanged += (bool isGrounded) =>
            {
                if (isGrounded)
                {
                    controller.characterStateMachine.ForceSetState(deadAnimState);
                }
            };
        }

        void FixedUpdate()
        {
            if (shouldRotate)
            {
                lerpT += turnSpeed * Time.fixedDeltaTime;
                Mathf.Clamp(lerpT, 0f, 1f);
                vCams[1].transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 0, 180f), lerpT);
            }

            if (lerpT >= 1)
            {
                shouldRotate = false;
                lerpT = 0;
                Debug.Log("done rot"); 
                
                StartCoroutine(WaitThenFall(1));
            }
        }
        
        IEnumerator WaitThenRotate(float seconds = 5f)
        {
            yield return new WaitForSeconds(seconds);
            RotateToHell();
        }
        
        IEnumerator WaitThenFall(float seconds = 2f)
        {
            yield return new WaitForSeconds(seconds);
            Physics2D.gravity = new Vector2(0, gravityFallSpeed);
            
            StartCoroutine(WaitThenAnimate(4f));
        }
        
        IEnumerator WaitThenAnimate(float seconds = 4f)
        {
            yield return new WaitForSeconds(seconds);
            controller.characterStateMachine.ForceSetState(deadAnimState);
            titleText.enabled = true;
            // TODO: add intense sound
            
            yield return new WaitForSeconds(5f);
            titleText.enabled = false;
        }
    }
}