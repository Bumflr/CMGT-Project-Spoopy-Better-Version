using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOver : MonoBehaviour
{
    [SerializeField] private GameObject bgObject;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;

        this.gameObject.SetActive(false);
        bgObject.SetActive(false);
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        if (newGameState == GameState.GameOver)
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

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    public void RestartGame()
    {
        GameManager.Instance.ReloadScene();
    }

}
