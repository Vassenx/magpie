using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Magpie
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private TMPro.TextMeshProUGUI titleText;
        [SerializeField] private OptionsMenu optionsMenu;
        [SerializeField] private Button mainMenuFirstSelected, optionMenuFirstSelected;
        [SerializeField] private List<Button> buttons;
        
        [SerializeField] private bool inGame;
        public InputSystemUIInputModule inputModule ;
        
        [SerializeField] private UIPreferences uiPrefs;
        [SerializeField] private PlayerInput input;
        
        private void Start()
        {
            optionsMenu.gameObject.SetActive(false);
            mainMenu.SetActive(!inGame);
            OnHighlightButton(buttons[0]);
        }
        
        private void OnEnable ()
        {
            input.SwitchCurrentActionMap("UI");
            inputModule.cancel.action.performed += Escape;
        }

        private void OnDisable()
        {
            input.SwitchCurrentActionMap("Player");
            inputModule.cancel.action.performed -= Escape;
        }

        private void Update()
        {
            if (!optionsMenu.isActiveAndEnabled && !mainMenu.activeInHierarchy && !inGame) // fallback
            {
                mainMenu.SetActive(true);
                
                if(titleText != null)
                    titleText.gameObject.SetActive(true);
            }
        }
        
        public void OnHighlightButton(Button button) // visuals update
        {
            bool enable = false;
            foreach (var curButton in buttons)
            {
                enable = button == curButton;
                var icon = curButton.GetComponentsInChildren<Image>(true).FirstOrDefault(x => x.gameObject != curButton.gameObject);
                icon?.gameObject.SetActive(enable);
                curButton.GetComponentInChildren<TextMeshProUGUI>().color = enable ? uiPrefs.buttonWhite : uiPrefs.buttonGrayed;
            }
        }

        public void OnContinueButtonClicked()
        {
            if (inGame)
            {
                mainMenu.SetActive(false);
                TogglePause(false);
            }
            else
            {
                SceneManager.LoadScene("Scenes/SampleScene"); // TODO: get scene the player is saved in
            }
        }
        
        public void OnNewGameButtonClicked() // TODO: confirmation pop up
        {
            SceneManager.LoadScene("Scenes/SampleScene"); // TODO: get starting game scene
        }
        
        public void OpenOptionsMenu()
        {
            mainMenu.SetActive(false);
            
            if (titleText != null)
            {
                titleText.gameObject.SetActive(false);
            }

            optionsMenu.gameObject.SetActive(true);
            
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionMenuFirstSelected.gameObject);
        }

        public void OnQuitButtonClicked()
        {
            PopUp.instance.DisplayPopUp(PopUpQuit, null);
        }

        public void Escape(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (optionsMenu.isActiveAndEnabled)
                {
                    PopUp.instance.DisplayPopUp(PopUpCloseOptions, buttons[0].gameObject);
                }
                else
                {
                    if (inGame)
                    {
                        mainMenu.SetActive(!mainMenu.activeInHierarchy);
                        TogglePause(mainMenu.activeInHierarchy);
                    }
                    else
                    {
                        Quit();
                    }
                }
            }
        }

        private void TogglePause(bool pause)
        {
            if (pause)
            {
                Time.timeScale = 0f;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected.gameObject);
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        
        public void PopUpCloseOptions(bool isConfirmed)
        {
            if (isConfirmed)
            {
                // TODO save settings
                optionsMenu.gameObject.SetActive(false);
                mainMenu.SetActive(true);
                
                if(titleText)
                    titleText.gameObject.SetActive(true);
                
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(mainMenuFirstSelected.gameObject);
            }
        } 
        
        public void PopUpQuit(bool isConfirmed)
        {
            if (isConfirmed)
            {
                if (inGame)
                {
                    // TODO save game
                    SceneManager.LoadScene("Scenes/StartScene"); // TODO
                }
                else
                {
                    Quit();
                }
            }
        } 
        
        public void Quit()
        {
            Application.Quit(); // TODO: confirmation pop up
        }
    }
}