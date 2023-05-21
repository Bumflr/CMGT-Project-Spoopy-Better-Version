using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CameraManagerScriptableObject", menuName = "ScriptableObjects/Camera Manager")]
public class SC_CameraManager_SO : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<float> lensChangeEvent;
    [System.NonSerialized] public UnityEvent<float> cameraShakeEvent;

    public float minimumLensSize = 4.2f;
    public float maximumLensSize = 6.0f;

    private float timeStamp;

    // Start is called before the first frame update
    void OnEnable()
    {
        timeStamp = 0;

        if (lensChangeEvent == null)
            lensChangeEvent = new UnityEvent<float>();
        if (cameraShakeEvent == null)
            cameraShakeEvent = new UnityEvent<float>();

        ChangeLensSize(maximumLensSize);
    }

    public void ChangeLensSize( float percentage)
    {
        var value = Mathf.Lerp(minimumLensSize, maximumLensSize, percentage);

        lensChangeEvent.Invoke(value);
    }

    //Uh i kinda just realized I dont have to do this shit lolol ah well
    public void ShakeCamera(float value)
    {
        if (timeStamp < Time.time)
        {
            timeStamp = Time.time + 0.5f;

            cameraShakeEvent.Invoke(value);
        }
    }
}
