using System.Collections;
using Animancer;
using UnityEngine;

namespace Magpie
{
    // TODO: animation set up, hide health bar on die, title pop up, etc
    public class FlipWorldMontage : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRb;
        [SerializeField] private GameObject[] vCams = new GameObject[2];
        [SerializeField] private PlayerController2D controller;
        [SerializeField] private DeadState deadAnimState;
        
        public float turnSpeed = 5;
        public float gravityFallSpeed = 25f;
        
        private bool shouldRotate = false;
        private float lerpT;

        private void Awake()
        {
            StartCoroutine(WaitThenRotate(3));
            lerpT = 0;
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
            
            //StartCoroutine(WaitThenAnimate(1));
        }
        
        /*IEnumerator WaitThenAnimate(float seconds = 4f)
        {
            yield return new WaitForSeconds(seconds);
            controller.characterStateMachine.ForceSetState(deadAnimState);
        }*/
    }
}