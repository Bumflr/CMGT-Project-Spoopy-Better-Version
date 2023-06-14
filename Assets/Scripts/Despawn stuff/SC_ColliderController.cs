using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the collider from the front
        if (IsPlayerEnteringFromFront(other))
        {
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exited the collider from the back
        if (IsPlayerExitingFromBack(other))
        {
            objectToActivate.SetActive(false);
        }
    }

    private bool IsPlayerEnteringFromFront(Collider other)
    {
        Vector3 playerDirection = other.transform.position - transform.position;
        Vector3 colliderForward = transform.forward;
        return Vector3.Dot(playerDirection, colliderForward) > 0f;
    }

    private bool IsPlayerExitingFromBack(Collider other)
    {
        Vector3 playerDirection = other.transform.position - transform.position;
        Vector3 colliderForward = transform.forward;
        return Vector3.Dot(playerDirection, colliderForward) < 0f;
    }
}