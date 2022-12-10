using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Magpie
{
    public class TutorialTextDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textMesh;

        private void Start()
        {
            textMesh.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
                textMesh.enabled = true;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
                textMesh.enabled = false;
        }
    }
}