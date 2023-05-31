using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{  
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Initialize();   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Saved Game!!!");
            DataPersistenceManager.instance.SaveGame();
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadArea(int areaIndex)
    {
        Debug.Log($"Loading {SceneManager.GetSceneByBuildIndex(areaIndex).name}...");
        SceneManager.LoadScene(areaIndex);
    }
}
