using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Death_Plane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("BITCH"); 
            GameStateManager.Instance.SetState(GameState.GameOver);
        }
    }
}
