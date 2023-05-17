using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CameraModifications : MonoBehaviour
{
    [Header("Dependencies")]
    public SC_CameraManager_SO cameraManagerScriptableObject;
    public CinemachineVirtualCamera vmCam;
    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        cameraManagerScriptableObject.lensChangeEvent.AddListener(ChangeLens);
        cameraManagerScriptableObject.cameraShakeEvent.AddListener(ShakeCamera);

    }

    private void OnDestroy()
    {
        cameraManagerScriptableObject.lensChangeEvent.RemoveListener(ChangeLens);
        cameraManagerScriptableObject.cameraShakeEvent.RemoveListener(ShakeCamera);
    }

    private void ChangeLens(float value)
    {
        vmCam.m_Lens.OrthographicSize = value;
    }
    private void ShakeCamera(float value)
    {
        impulseSource.GenerateImpulseWithForce(value);
    }

}
