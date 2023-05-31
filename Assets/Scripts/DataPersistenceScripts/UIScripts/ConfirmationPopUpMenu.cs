using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ConfirmationPopUpMenu : MonoBehaviour
{
    [SerializeField] private Text displayText;

    //[SerializeField] private MenuControls menuControls;

    [SerializeField] private Button confirmButton;
    [SerializeField] public Button cancelButton;

    public void ActivateMenu(string displayText, UnityAction confirmAction, UnityAction cancelAction)
    {
        this.gameObject.SetActive(true);
        //menuControls.SetFirstButton(cancelButton.gameObject);

        this.displayText.text = displayText;
        //remove any existing listeners just to make sure there aren't any previous ones hangin around
        //note - this only removes listeners added through code
        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            DeactivateMenu();
            confirmAction();
        });
        cancelButton.onClick.AddListener(() =>
        {
            DeactivateMenu();
            cancelAction();
        });
    }
    private void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}

