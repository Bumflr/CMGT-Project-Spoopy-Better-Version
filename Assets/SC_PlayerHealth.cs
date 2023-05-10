using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public enum HealthState
{
    Fine,
    Hurt,
    Wounded,
    Dead,
}

public class SC_PlayerHealth : MonoBehaviour
{
    static HealthState[] messages =
    {
        HealthState.Dead,
        HealthState.Wounded,
        HealthState.Wounded,
        HealthState.Wounded,
        HealthState.Wounded,
        HealthState.Hurt,
        HealthState.Hurt,
        HealthState.Hurt,
        HealthState.Hurt,
        HealthState.Fine,
    };

    [SerializeField] UI_Health healthUI;

    static int NUM_States = messages.Length;

    public int health;

    private float timer;
    public float offset = 2f;

    private void Awake()
    { 
        health = messages.Length;
    }

    public void RestoreHealth(int amount) { health += amount; }
    public void TakeHealth(int amount) { health -= amount; }

    private void Update()
    {
        CheckCondition();
    }
    void CheckCondition()
    {
        if (health > NUM_States - 1)
        {
            healthUI.SetHealthIcon(messages[NUM_States - 1]);
        }
        else if (health <= 0)
        {
            healthUI.SetHealthIcon(messages[0]);

            //Play like a fancy death animation here-o
            GameStateManager.Instance.SetState(GameState.GameOver);
        }
        else
        {
            healthUI.SetHealthIcon(messages[health]);
        }

    }

    public void BeingHeld()
    {
        if (timer <= Time.time)
        {
            timer = Time.time + offset;
            TakeHealth(1);
        }
    }
}
