using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_LoadNewArea : MonoBehaviour
{
    [HideInInspector] public string[] scenes;
    [HideInInspector] public int selected = 0;

    //I handle the custom enum popup in the Unity Inspector in another script called SC_LoadNewAreaEditor

    private void OnValidate()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        scenes = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoadArea(selected);
        }
    }
}

