using UnityEngine;

public class SC_DoorMechanic : MonoBehaviour
{
    public bool correctDoor;

    [SerializeField] private Transform teleportDestination;
    private SC_HallwayManager hallwayManager;
    
    //NOTE: There needs to be some variable that changes the player's teleportation desitation depending on the door the player collided with

    private void Start()
    {
        // Get the SC_HallwayManager component
        hallwayManager = FindObjectOfType<SC_HallwayManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<SC_PlayerMovement>(out var playerMovement))
        {
            if (correctDoor)
            {
                hallwayManager.correctChoicesCount++;
                // Switch the correct door in the SC_HallwayManager
                hallwayManager.SwitchCorrectDoor();

                //Teleport the player

                if (hallwayManager.correctChoicesCount < 3)
                {
                    playerMovement.Teleport(teleportDestination.position);
                }
                //Make em pass through without a problem because they did the thing :DDD
            }
            else
            {
                // Player chose the wrong door
                // Implement your desired behavior here
                Debug.Log("Ha you chose the wrong door idiot");

                playerMovement.Teleport(teleportDestination.position);

            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(teleportDestination.position, .4f);
        var direction = teleportDestination.TransformDirection((Vector3.forward));
        Gizmos.DrawRay(teleportDestination.position, direction);
    }
}