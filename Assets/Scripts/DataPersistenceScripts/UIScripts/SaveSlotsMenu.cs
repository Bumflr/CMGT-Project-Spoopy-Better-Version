using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : MonoBehaviour
{
    private SaveSlot[] saveSlots;

    [SerializeField] private Button backButton;

    [SerializeField] private ConfirmationPopUpMenu confirmationPopUpMenu;

    [SerializeField] public bool isInConfirmationMenu { get; private set; }

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    private void OnEnable()
    {
        ActivateMenu();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        //disable all buttons
        DisableMenuButtons();

        //update the selected profile id to be used for data persistence
        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileID());

        //If there is no data at this slot make new data
        if (!saveSlot.hasData)
        {
            // create a new game - which will initialize our data to a clean slate
            DataPersistenceManager.instance.NewGame();
        }
        SaveGameAndLoadScene();
    }

    public void SaveGameAndLoadScene()
    {
        DataPersistenceManager.instance.SaveGame();
        //Load the scene
        SceneManager.LoadSceneAsync("Mines");
    }

    public void OnClearClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();

        isInConfirmationMenu = true;

        confirmationPopUpMenu.ActivateMenu(
            "Are you sure you want to delete this save file?",
            //function to execute if we select 'yes'
            () => {
                DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileID());
                EnableMenuButtons();
                ActivateMenu();
                //TitleMenuManager.ReloadMenu();
            },
            //function to execute if we select 'cancel'
            () => {
                EnableMenuButtons();
                ActivateMenu();
                //TitleMenuManager.ReloadMenu();
            }
         );

    }

    public void ActivateMenu()
    {
        isInConfirmationMenu = false;

        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);
        }
    }

    private void EnableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(true);
        }
        //ensure the back button is enabled when we activate the menu
        backButton.interactable = true;
    }
    private void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }
}
