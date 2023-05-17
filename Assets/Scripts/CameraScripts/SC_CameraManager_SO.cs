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

    // Start is called before the first frame update
    void OnEnable()
    {
        if (lensChangeEvent == null)
            lensChangeEvent = new UnityEvent<float>();

        if (cameraShakeEvent == null)
            cameraShakeEvent = new UnityEvent<float>();
    }

    public void ChangeLensSize( float percentage)
    {
        var value = Mathf.Lerp(minimumLensSize, maximumLensSize, percentage);

        lensChangeEvent.Invoke(value);
    }

    public void ShakeCamera(float value)
    {
        cameraShakeEvent.Invoke(value);
    }
}
