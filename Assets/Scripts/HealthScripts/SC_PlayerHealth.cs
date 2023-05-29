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
    [SerializeField]
    private SC_Health_SO healthManager;

    private float timer;
    public float offset = 2f;

    public void BeingHeld()
    {
        if (timer <= Time.time)
        {
            timer = Time.time + offset;
            healthManager.TakeHealth(1);
        }
    }
}
