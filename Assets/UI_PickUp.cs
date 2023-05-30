using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PickUp : MonoBehaviour
{
    [Header("Dependencies")]
    public SC_PickUp_SO pickUpManager;

    public TextMeshProUGUI nameObject;
    public Image spriteObject;
    public TextMeshProUGUI descriptionObject;
    public GameObject bgObject;

    private void Awake()
    {
        pickUpManager.pickUpEvent.AddListener(SetUpPickUpScreen);

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;

        this.gameObject.SetActive(false);
        bgObject.SetActive(false);
    }

    private void OnDestroy()
    {
        pickUpManager.pickUpEvent.RemoveListener(SetUpPickUpScreen);
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.PickUpItemScreen)
        {
            this.gameObject.SetActive(true);
            bgObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            bgObject.SetActive(false);
        }
    }

    private void SetUpPickUpScreen(string name, Sprite graphic, string description)
    {
        nameObject.text = name;
        spriteObject.sprite = graphic;
        descriptionObject.text = description;
    }
}
