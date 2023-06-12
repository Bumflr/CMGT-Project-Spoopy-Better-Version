using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_TitleScreen : MonoBehaviour
{
    [Header("Dependencies")]
    public GameObject SaveSelectMenu;
    public GameObject SettingsMenu;
    public SC_LoadNewArea loadNewArea;

    public void StartGame()
    {
        Debug.Log("Opening Save File Menu");
        SaveSelectMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void BackToTitle()
    {
        Debug.Log("Opening Title Menu");
        SaveSelectMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        this.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Qutting game...");

        Application.Quit();
    }
}
