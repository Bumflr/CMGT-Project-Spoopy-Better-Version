using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayScript : MonoBehaviour
{
    AudioSource audioSource;

    float lastTimePlayed;
    float audioSourceLength;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetSoundClip(SoundManager.Sound sound)
    {
        audioSource.clip = SoundManager.GetAudioClip(sound);
        audioSource.volume = SoundManager.GetVolume(sound);
        audioSource.outputAudioMixerGroup = SoundManager.GetAudioMixerGroup(sound);
    }

    public void Start()
    {
        audioSource.Play();

        audioSourceLength = audioSource.clip.length;
        lastTimePlayed = Time.time;
    }

    public void Update()
    {
        if (lastTimePlayed + audioSourceLength < Time.time)
        {
            this.gameObject.SetActive(false);
        }
    }
}
