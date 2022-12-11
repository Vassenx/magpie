using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Magpie
{
    [CreateAssetMenu(fileName = "UIPreferences", menuName = "UIPreferences", order = 1)]
    public class UIPreferences : ScriptableObject
    {
        [Header("Buttons")] 
        public Color buttonTextColor;
        public Color buttonHighlightTextColor;
        public Color buttonPickedTextColor;
        public Color buttonPickedImageColor;
        public Color buttonWhite;
        public Color buttonGrayed;

        #region singleton
        
        static UIPreferences instance;
 
        public UIPreferences Instance
        {
            get
            {
                if (instance != null && instance != this) 
                { 
                    Destroy(this); 
                } 
                else 
                { 
                    instance = this; 
                } 
 
                return instance;
            }
        }
        
        #endregion

        public void ChangeToNormalButton(Button button)
        {
            Image image = button.image;
            if (image != null)
            {
                // just in case this func is called outside of the standard button way
                image.color = button.colors.normalColor;
            }

            button.GetComponentInChildren<TextMeshProUGUI>().color = buttonTextColor; // ugh GetComponent
        }
        
        public void ChangeToHighlightButton(Button button)
        {
            Image image = button.image;
            if (image != null)
            {
                if (image.color != buttonPickedTextColor)
                {
                    image.color = button.colors.highlightedColor; 
                }
            }
            
            button.GetComponentInChildren<TextMeshProUGUI>().color = buttonHighlightTextColor;
        }
        
        public void ChangeToPickedButton(Button button)
        {
            Image image = button.image;
            if (image != null)
            {
                image.color = buttonPickedImageColor;
            }
            
            button.GetComponentInChildren<TextMeshProUGUI>().color = buttonPickedTextColor;
        }
    }
}