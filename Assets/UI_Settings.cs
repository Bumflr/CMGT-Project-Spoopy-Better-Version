using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Settings : MonoBehaviour
{
    public GameObject[] Panels;

    public void OpenPanel(GameObject currentPanel)
    {
        foreach (GameObject panel in Panels)
        {
            panel.SetActive(false);
        }

        if (currentPanel != null)
        {
            bool isActive = currentPanel.activeSelf;

            currentPanel.SetActive(!isActive);
        }
    }
}
