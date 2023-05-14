using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class SC_InputManager
{
    static Dictionary<string, KeyCode[]> buttonKeys = new Dictionary<string, KeyCode[]>();

    static Dictionary<string, KeyCode> pcKey = new Dictionary<string, KeyCode>();

    static Dictionary<string, KeyCode> controllerKey = new Dictionary<string, KeyCode>();

    // Start is called before the first frame update
    public static void Initialize()
    {
        //Todo rework the entire playerprefs system and tie it directly to the save system by making a new fourth save file that stores the settings
        //Cuz jesus christ wtf Is this
        buttonKeys["Jump"] = new KeyCode[] { (KeyCode)PlayerPrefs.GetInt("JumpFalse"), (KeyCode)PlayerPrefs.GetInt("JumpTrue") };
        buttonKeys["Attack"] = new KeyCode[] { (KeyCode)PlayerPrefs.GetInt("AttackFalse"), (KeyCode)PlayerPrefs.GetInt("AttackTrue") };
        buttonKeys["Magic"] = new KeyCode[] { (KeyCode)PlayerPrefs.GetInt("MagicFalse"), (KeyCode)PlayerPrefs.GetInt("MagicTrue") };
        buttonKeys["Pause"] = new KeyCode[] { (KeyCode)PlayerPrefs.GetInt("PauseFalse"), (KeyCode)PlayerPrefs.GetInt("PauseTrue") };
        buttonKeys["Dodge"] = new KeyCode[] { (KeyCode)PlayerPrefs.GetInt("DodgeFalse"), (KeyCode)PlayerPrefs.GetInt("DodgeTrue") };
        buttonKeys["Transform"] = new KeyCode[] { (KeyCode)PlayerPrefs.GetInt("TransformFalse"), (KeyCode)PlayerPrefs.GetInt("TransformTrue") };
        buttonKeys["Submit"] = new KeyCode[] { (KeyCode)PlayerPrefs.GetInt("SubmitFalse"), (KeyCode)PlayerPrefs.GetInt("SubmitTrue") };
        buttonKeys["Cancel"] = new KeyCode[] { (KeyCode)PlayerPrefs.GetInt("CancelFalse"), (KeyCode)PlayerPrefs.GetInt("CancelTrue") };
    }

    public static bool GetButtonDown(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager::GetButtonDown -- No Button named: " + buttonName);
            return false;
        }

        //Maybe I need to change this, but we'll see
        pcKey[buttonName] = buttonKeys[buttonName].First();

        controllerKey[buttonName] = buttonKeys[buttonName].Last();

        return Input.GetKeyDown(pcKey[buttonName]) || Input.GetKeyDown(controllerKey[buttonName]);
    }

    public static bool GetButton(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager::GetButtonDown -- No Button named: " + buttonName);
            return false;
        }

        //Maybe I need to change this, but we'll see
        pcKey[buttonName] = buttonKeys[buttonName].First();

        controllerKey[buttonName] = buttonKeys[buttonName].Last();

        return Input.GetKey(pcKey[buttonName]) || Input.GetKey(controllerKey[buttonName]);
    }

    public static bool GetButtonUp(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager::GetButtonDown -- No Button named: " + buttonName);
            return false;
        }

        //Maybe I need to change this, but we'll see
        pcKey[buttonName] = buttonKeys[buttonName].First();

        controllerKey[buttonName] = buttonKeys[buttonName].Last();

        return Input.GetKeyUp(pcKey[buttonName]) || Input.GetKeyUp(controllerKey[buttonName]);
    }

    public static float GetAxisRaw(string axisName)
    {
        return 0;
    }

    public static string[] GetButtonNames()
    {
        //To array uses (O)n notation
        return buttonKeys.Keys.ToArray();
    }
    public static string GetKeyNamesForButton(string buttonName, int index)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager::GetButtonDown -- No Button named: " + buttonName);
            return null;
        }

        if (index == 0)
        {
            return buttonKeys[buttonName].First().ToString();
        }
        else
        {
            return buttonKeys[buttonName].Last().ToString();
        }
    }

    public static void SetButtonsForKey(string buttonName, KeyCode[] keyCode, bool isController)
    {
        if (!isController)
        {
            keyCode[1] = buttonKeys[buttonName].Last();
            buttonKeys[buttonName] = keyCode;

            Debug.Log(buttonKeys[buttonName]);
        }
        if (isController)
        {
            //Set the first ACTUAL keyCode to the second one
            keyCode[1] = keyCode[0];
            //And then set the keycode you want to keep the same to the first one
            keyCode[0] = buttonKeys[buttonName].First();

            buttonKeys[buttonName] = keyCode;

            Debug.Log(buttonKeys[buttonName]);
        }
    }


}
