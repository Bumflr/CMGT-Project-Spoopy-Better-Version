using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI TextComponent;
    public string[] lines;
    public float textspeed;

    private int index; 

    // Start is called before the first frame update
    void Start()
    {
        TextComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (TextComponent.text == lines[index])
            {
                Nextline();
            }
            else
            {
                StopAllCoroutines();
                TextComponent.text = lines[index];
            }
        }
    }

    void StartDialogue() 
    { 
        index = 0;
        StartCoroutine(Typeline());
    }

    IEnumerator Typeline()
    {
        //type characters 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            TextComponent.text += c; 
            yield return new WaitForSeconds(textspeed);
        }
    }

    void Nextline()
    {
        if(index < lines.Length - 1)
        {
            index++;
            TextComponent.text = string.Empty;
            StartCoroutine(Typeline()); 
        }
        else
        {
            gameObject.SetActive(false); 
        }
    }

}
