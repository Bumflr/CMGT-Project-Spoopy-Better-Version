using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private SC_Health_SO healthManagerScriptableObject;
    public Image healthIcon;

    [Header("DisplaySprites")]
    public Sprite fineSprite;
    public Sprite hurtSprite;
    public Sprite woundedSprite;
    public Sprite deadSprite;

 
    private void Awake()
    {
        healthManagerScriptableObject.healthChangeEvent.AddListener(SetHealthIcon);
    }
    private void OnEnable()
    {
        healthManagerScriptableObject.RestoreHealth(0);
    }

    private void OnDestroy()
    {
        healthManagerScriptableObject.healthChangeEvent.RemoveListener(SetHealthIcon);
    }

    public void SetHealthIcon(HealthState healthState)
    {
        Debug.Log(healthState);

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
