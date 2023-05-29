using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HearingScriptableObjecy", menuName = "ScriptableObjects/Hearing Manager")]
public class SC_HearingManager_SO : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<Vector3, float> hearingEvent;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (hearingEvent == null)
            hearingEvent = new UnityEvent<Vector3, float>();
    }

    public void MakeASound(Vector3 position, float volume) { hearingEvent.Invoke(position, volume); }
}
