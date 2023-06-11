using UnityEngine;

public class HallwayManager : MonoBehaviour
{
    public GameObject[] doors;
    private GameObject correctDoor;

    private void Start()
    {
        // Assign the initial correct door randomly from the available doors
        correctDoor = doors[Random.Range(0, doors.Length)];
        correctDoor.tag = "CorrectDoor";
    }

    public void SwitchCorrectDoor()
    {
        // Find the current correct door and remove the "CorrectDoor" tag
        foreach (GameObject door in doors)
        {
            if (door.CompareTag("CorrectDoor"))
            {
                door.tag = "Untagged";
                break;
            }
        }

        // Randomly select a new correct door from the other doors
        GameObject newCorrectDoor;
        do
        {
            newCorrectDoor = doors[Random.Range(0, doors.Length)];
        } while (newCorrectDoor == correctDoor);

        // Assign the "CorrectDoor" tag to the new correct door
        newCorrectDoor.tag = "CorrectDoor";
        correctDoor = newCorrectDoor;
    }
}