using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class SC_BigDoor : MonoBehaviour
{
    [Header("Dependencies")]
    public SC_PlayerController playerController;

    public GameObject obJectWithAnimation;
    public GameObject bigDoor;
    public GameObject explosion;

    public bool isTouchingDoor =  true;

    private void Awake()
    {
        bigDoor = GameObject.FindGameObjectWithTag("Door");
    }

    public void BlowUpDoor(int amountOfDynamite)
    {
        if (amountOfDynamite >= 2 && isTouchingDoor)
        {
            Debug.Log("Kablooey!");

            GameStateManager.Instance.SetState(GameState.Gameplay);

            Item item = new Item { itemType = ItemType.DynamiteStaff, amount = amountOfDynamite };

            playerController.playerInventory.RemoveItem(item);

            playerController.playerMovementScript.playerMovement._cam.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Follow = obJectWithAnimation.transform;

            obJectWithAnimation.GetComponent<Animator>().enabled = true;

            SoundManager.PlaySound(SoundManager.Sound.Explosion);


            StartCoroutine(WaitLike5Seconds());

        }
        else
        {
            Debug.Log("Lol you stupid bastard");
        }
    }
    private IEnumerator WaitLike5Seconds()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadSceneAsync("Outro");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            isTouchingDoor = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            isTouchingDoor = false;
        }
    }
}
