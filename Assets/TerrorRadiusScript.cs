using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrorRadiusScript : MonoBehaviour
{
    //Honestly I should do this via a Physics.OVerlapSphere
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            SoundManager.PlaySound(SoundManager.Sound.TerrorRadiusSound, this.transform.position);
        }
    }

}
