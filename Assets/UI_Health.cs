using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    public Sprite fineSprite;
    public Sprite hurtSprite;
    public Sprite woundedSprite;
    public Sprite deadSprite;


    public Image healthIcon;

    public void SetHealthIcon(HealthState healthState)
    {
        switch (healthState)
        {
            case HealthState.Fine:
                healthIcon.sprite = fineSprite;
                break;
            case HealthState.Hurt:
                healthIcon.sprite = hurtSprite;
                break;
            case HealthState.Wounded:
                healthIcon.sprite = woundedSprite;
                break;
            case HealthState.Dead:
                healthIcon.sprite = deadSprite;
                break;
            default:
                healthIcon.sprite = fineSprite;
                break;
        }
    }

}
