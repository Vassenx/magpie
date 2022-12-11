using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace Magpie
{
    // Abstract pop up, use default prefab for consistent style between all pop ups in game
    public class PopUp : MonoBehaviour
    {
        [SerializeField] private GameObject popUpObj;
        [SerializeField] private Image hiddenOverlayPanel;
        [SerializeField] private TextMeshProUGUI popUpText;
        
        [Header("Buttons")]
        [SerializeField] private Button popUpYesButton;
        [SerializeField] private Button popUpNoButton;
        [SerializeField] private TextMeshProUGUI noText;
        [SerializeField] private TextMeshProUGUI yesText;
        private GameObject selectableAfterExit;
        private GameObject lastSelected;
        private Action<bool> callback;
        public static PopUp instance;
        
        private void Awake()
        {
            #region singleton

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            #endregion
            
            popUpObj.SetActive(false);
        }

        public bool IsPopUpActive()
        {
            return (popUpObj.activeInHierarchy);
        }
    
        public void DisplayPopUp(Action<bool> newCallback, GameObject selectedGameObjectAfterClosePopUp, string message = "Are you sure?", string confirmButtonText = "Yes", string denyButtonText = "No")
        {
            if(IsPopUpActive()) // only one popup at a time
                return;
                
            popUpObj.SetActive(true);
            popUpText.text = message;
        
            yesText.text = confirmButtonText;
            noText.text = denyButtonText;

            selectableAfterExit = selectedGameObjectAfterClosePopUp;
            lastSelected = EventSystem.current.currentSelectedGameObject;
            SetButtonNavigation();
            
            this.callback = newCallback;
        }

        private void ClosePopUp(bool isConfirmed)
        {
            popUpObj.SetActive(false);
        
            popUpYesButton.onClick.RemoveAllListeners();
            popUpNoButton.onClick.RemoveAllListeners();
            
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(isConfirmed ? selectableAfterExit.gameObject : lastSelected);
        }

        public void OnClickPopUpButton(bool isConfirming)
        {
            callback(isConfirming);
            ClosePopUp(isConfirming);
        }

        private void SetButtonNavigation()
        {
            popUpYesButton.onClick.AddListener(delegate { OnClickPopUpButton(true); } );
            popUpNoButton.onClick.AddListener(delegate { OnClickPopUpButton(false); } );
            
            Navigation customNoNav = new Navigation();
            customNoNav.mode = Navigation.Mode.Explicit;
            customNoNav.selectOnLeft = popUpYesButton;
            popUpNoButton.navigation = customNoNav;
            
            Navigation customYesNav = new Navigation();
            customYesNav.mode = Navigation.Mode.Explicit;
            customYesNav.selectOnRight = popUpNoButton;
            popUpYesButton.navigation = customYesNav;
            
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(popUpNoButton.gameObject); // TODO: button to go back to?
        }
    }
}