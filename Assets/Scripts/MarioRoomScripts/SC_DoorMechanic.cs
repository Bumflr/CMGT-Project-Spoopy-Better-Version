using UnityEngine;

public class SC_DoorMechanic : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    private GameObject currentRoom;

    [SerializeField] private Transform teleportDestination;
    private int correctChoicesCount = 0;

    private SC_HallwayManager hallwayManager;
    
    //NOTE: There needs to be some variable that changes the player's teleportation desitation depending on the door the player collided with

    private void Start()
    {
        // Instantiate the initial room
        currentRoom = Instantiate(roomPrefabs[0], transform.position, Quaternion.identity);

        // Get the SC_HallwayManager component
        hallwayManager = FindObjectOfType<SC_HallwayManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<SC_PlayerMovement>(out var playerMovement))
        {
            // Randomly select the next room prefab
            GameObject nextRoomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];

            if (other.gameObject.CompareTag("CorrectDoor"))
            {
                // Player chose the correct door
                Destroy(currentRoom);
                currentRoom = Instantiate(nextRoomPrefab, transform.position, Quaternion.identity);

                correctChoicesCount++;

                // Switch the correct door in the SC_HallwayManager
                hallwayManager.SwitchCorrectDoor();
                
                //Teleport the player
                playerMovement.Teleport(teleportDestination.position);
            }
            else
            {
                // Player chose the wrong door
                // Implement your desired behavior here
            }

            // Check if the player has made the correct choice three times
            if (correctChoicesCount >= 3)
            {
                // Player has broken out of the cycle
                // Implement your desired behavior here
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