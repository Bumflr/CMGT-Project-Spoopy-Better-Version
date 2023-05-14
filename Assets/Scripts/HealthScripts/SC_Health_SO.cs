using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="HealthScriptableObject", menuName = "ScriptableObjects/Health Manager")]
public class SC_Health_SO : ScriptableObject
{
    static HealthState[] healthStateTable =
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

    static int healthCounter = healthStateTable.Length;

    [ReadOnly] public int health = healthCounter;

    [System.NonSerialized] public UnityEvent<HealthState> healthChangeEvent;

    private void OnEnable()
    {
        health = healthCounter;
        if (healthChangeEvent == null)
            healthChangeEvent = new UnityEvent<HealthState>();


    }

    public void RestoreHealth(int amount) { health += amount; CheckCondition(); }
    public void TakeHealth(int amount) { health -= amount; CheckCondition(); }

    private void CheckCondition()
    {
        if (health > healthCounter - 1)
        {
            healthChangeEvent.Invoke(healthStateTable[healthCounter - 1]);
        }
        else if (health <= 0)
        {
            healthChangeEvent.Invoke(healthStateTable[0]);

            //We want the heatlh to go back to 10 when exiting the game
            //But not when we are switching scenes
            //We do want the health to go back to 10 when dying tho
            //Thas why im settin it here
            health = healthCounter;

            GameStateManager.Instance.SetState(GameState.GameOver);
        }
        else
        {
            healthChangeEvent.Invoke(healthStateTable[health]); 
        }
    }

}
