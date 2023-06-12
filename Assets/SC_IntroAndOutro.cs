using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_IntroAndOutro : MonoBehaviour
{
    [Header("Dependencies")]
    public SC_Notes_SO notesManager;
    public bool intro;

    public GameObject AudioLog;
    // Start is called before the first frame update
    void Start()
    {
        if (intro)
        {
            Item item = new Item { amount = 1, itemType = ItemType.Intro };

            notesManager.StartReadingItem(item);
        }
        else
        {
            Item item = new Item { amount = 1, itemType = ItemType.Outro };

            notesManager.StartReadingItem(item);

        } 
    }

    private void Update()
    {
        if (!AudioLog.activeSelf)
        {
            if (intro)
            {
                SceneManager.LoadScene("Mines");
            }
            else
            {
                SceneManager.LoadScene("end credit scene");

            }
        }
    }
}
