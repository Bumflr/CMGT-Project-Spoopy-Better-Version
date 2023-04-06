using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundAsset : MonoBehaviour
{
    private static SoundAsset _Instance;

    public static SoundAsset Instance
    {
        get { if (_Instance == null) _Instance = Instantiate(Resources.Load<SoundAsset>("SoundsDatabase")) ;return _Instance; }
    }

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
        public float volume;
        public AudioMixerGroup audioMixerGroup;
    }

   
}
