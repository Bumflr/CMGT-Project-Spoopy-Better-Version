using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum LightingZones
{
    Normal,
    TotalDarkness,
    SafeArea
}
public class SC_LightModifications : MonoBehaviour
{
    [Header("Dependencies")]
    public Light Light;
    [Header("Settings")]

    public Color normalColor;
    public Color totalDarknessColor;
    public Color safeAreaColor;

    [Range(0f, 1f)]
    public float amountOftime;

    [ReadOnly] public Color currentColor;
    private void Awake()
    {
        Light = GetComponent<Light>();
        currentColor = normalColor;
    }


    void ChangeLight(LightingZones color)
    {
        switch (color)
        {
            case LightingZones.Normal:
                currentColor = normalColor;
                break;
                case LightingZones.TotalDarkness:
                currentColor = totalDarknessColor;
                break;
                case LightingZones.SafeArea:
                currentColor = safeAreaColor;
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeLight(LightingZones.SafeArea);
        }

        var lerpedColour = Color.Lerp(Light.color, currentColor, amountOftime * Time.deltaTime);

        Light.color = lerpedColour;
    }
}
