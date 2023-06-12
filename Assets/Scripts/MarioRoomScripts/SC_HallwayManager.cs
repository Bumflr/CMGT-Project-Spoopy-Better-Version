using System;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class SC_HallwayManager : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public Transform roomPosition;
    private GameObject currentRoom;

    public GameObject[] doors; //Initiliaze this shit manually
    public int correctChoicesCount = 0;
    private SC_DoorMechanic[] doorMechanics;

    private GameObject correctDoor;

    private void Start()
    {
        // Instantiate the initial room
        currentRoom = Instantiate(roomPrefabs[0], roomPosition.position, Quaternion.identity);

        doorMechanics = new SC_DoorMechanic[doors.Length];

        for (int i = 0; i < doors.Length; i ++)
        {
            doorMechanics[i] = doors[i].GetComponent<SC_DoorMechanic>();
        }

        // Assign the initial correct door randomly from the available doors
        correctDoor = doors[UnityEngine.Random.Range(0, doors.Length)];
        correctDoor.GetComponent<SC_DoorMechanic>().correctDoor = true;
    }

    public void SwitchCorrectDoor()
    {
        SpawnNextRoom();

        // Find the current correct door and remove the "CorrectDoor" tag
        foreach (SC_DoorMechanic door in doorMechanics)
        {
            if (door.correctDoor)
            {
                door.correctDoor = false;
                break;
            }
        }

        SC_DoorMechanic newCorrectDoor;
        newCorrectDoor = doorMechanics[UnityEngine.Random.Range(0, doors.Length)];

        newCorrectDoor.correctDoor = true;
        correctDoor = newCorrectDoor.gameObject;
    }

    public void SpawnNextRoom()
    {
        // Randomly select the next room prefab
        GameObject nextRoomPrefab = roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Length)];
        Destroy(currentRoom);
        currentRoom = Instantiate(nextRoomPrefab, roomPosition.position, Quaternion.identity);

    }
}