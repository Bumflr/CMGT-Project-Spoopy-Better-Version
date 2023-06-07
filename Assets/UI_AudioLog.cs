using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_AudioLog : MonoBehaviour
{
    [Header("Dependencies")]
    public SC_Notes_SO notesManager;

    public TextMeshProUGUI nameLog;
    public Image spriteObject;
    public TextMeshProUGUI displayText;
    public GameObject bgObject;
    public GameObject leftIndicator;
    public GameObject rightIndicator;

    [ReadOnly, TextArea, SerializeField]
    private string[] audioLogText;
    private int pageNumber;

    private void Awake()
    {
        notesManager.startReadEvent.AddListener(SetUpReadScreen);

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;

        this.gameObject.SetActive(false);
        bgObject.SetActive(false);
    }

    private void OnDestroy()
    {
        notesManager.startReadEvent.RemoveListener(SetUpReadScreen);
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.ReadScreen)
        {
            this.gameObject.SetActive(true);
            bgObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            bgObject.SetActive(false);
            leftIndicator.SetActive(false);
            rightIndicator.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && GameStateManager.Instance.CurrentGameState == GameState.ReadScreen) 
        {
            pageNumber++;
            if (pageNumber > audioLogText.Length - 1)
            {
                GameStateManager.Instance.SetState(GameState.Gameplay);
            }
            else
            {
                SetCurrentPage(pageNumber);

            }
        }

        if (Input.GetKeyDown(KeyCode.A) && GameStateManager.Instance.CurrentGameState == GameState.ReadScreen)
        {
            if (pageNumber - 1 >= 0)
            {
                pageNumber--;


                SetCurrentPage(pageNumber);
            } 
        }
    }

    private void SetUpReadScreen(string name, Sprite graphic, string[] text)
    {
        pageNumber = 0;
        nameLog.text = name;
        spriteObject.sprite = graphic;
        audioLogText = text;
        leftIndicator.SetActive(false);

        SetCurrentPage(pageNumber);
    }

    private void SetCurrentPage(int currentPage)
    {
        if (currentPage == 0)
            leftIndicator.SetActive(false);
        else
            leftIndicator.SetActive(true);


        if (currentPage == audioLogText.Length -1)
            rightIndicator.SetActive(false);
        else
            rightIndicator.SetActive(true);


        displayText.text = audioLogText[currentPage];
    }
}
