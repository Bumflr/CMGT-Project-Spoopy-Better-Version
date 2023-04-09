using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private GameObject player;

    public GameState currentGameState;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        GameState currentGameState = GameStateManager.Instance.CurrentGameState;

        GameState newGameState = GameState.Gameplay;

        GameStateManager.Instance.SetState(newGameState);
    }

    void Update()
    {
        currentGameState = GameStateManager.Instance.CurrentGameState;

        if (Input.GetButtonDown("Cancel"))
        {
            if (GameStateManager.Instance.CurrentGameState == GameState.Paused)
            {
                GameStateManager.Instance.SetState(GameState.Gameplay);
            }
            else if (GameStateManager.Instance.CurrentGameState == GameState.Gameplay)
            {
                GameStateManager.Instance.SetState(GameState.Paused);
            }
        }
    }
}
