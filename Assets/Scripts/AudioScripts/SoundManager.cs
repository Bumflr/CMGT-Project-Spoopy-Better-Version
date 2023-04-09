using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Audio;
using static SoundManager;
using System.Linq;

public static class SoundManager
{
    public enum Sound
    {
        DetectingGhost,
        GhostIdle,
        GhostChase,
        TerrorRadiusSound
    }


    private static Dictionary<Sound, float> soundTimerDictionary;

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    private static GameObject bgmGameObject;
    private static AudioSource bgmAudioSource;


    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();

        //bruh you can't iterate over Dictionary's apparantly so for every Sound that will be played we need to do = 0f;
        soundTimerDictionary[Sound.DetectingGhost] = Time.time;
        soundTimerDictionary[Sound.GhostIdle] = Time.time;
        soundTimerDictionary[Sound.GhostChase] = Time.time;
        soundTimerDictionary[Sound.TerrorRadiusSound] = Time.time;
    }

    // TODO adding VOLUME with the audio groups
    // For reference, look at the gamemanager script which ahas a quick solution to do this.


    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;

            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 25f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;

            audioSource.volume = GetVolume(sound);
            audioSource.outputAudioMixerGroup = GetAudioMixerGroup(sound);

            audioSource.Play();

            UnityEngine.Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.volume = GetVolume(sound);
            oneShotAudioSource.outputAudioMixerGroup = GetAudioMixerGroup(sound);
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    public static void PlayMusic(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (bgmGameObject == null)
            {
                bgmGameObject = new GameObject("Music Sound");
                UnityEngine.Object.DontDestroyOnLoad(bgmGameObject);
                bgmAudioSource = bgmGameObject.AddComponent<AudioSource>();
            }

            if (bgmAudioSource.clip == null)
            {
                bgmAudioSource.clip = GetAudioClip(sound);
                bgmAudioSource.outputAudioMixerGroup = GetAudioMixerGroup(sound);
                bgmAudioSource.volume = GetVolume(sound);
                bgmAudioSource.loop = true;
                bgmAudioSource.Play();
            }
            else if (bgmAudioSource.clip != GetAudioClip(sound))
            {
                bgmAudioSource.clip = GetAudioClip(sound);
                bgmAudioSource.outputAudioMixerGroup = GetAudioMixerGroup(sound);
                bgmAudioSource.volume = GetVolume(sound);
                bgmAudioSource.loop = true;
                bgmAudioSource.Play();
            }
            else
            {
                Debug.Log("Tried to play the same music track twice.");
            }
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            //This code doesnt work that well, it doesnt start playing any sound until the TimerMax time so if there is a 
            //particurarly long sound effect it only starts playing 11 seconds in
            case Sound.DetectingGhost:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float TimerMax = 1f;

                    if (lastTimePlayed + TimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            case Sound.GhostIdle:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float TimerMax = 4f;
                    if (lastTimePlayed + TimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            case Sound.TerrorRadiusSound:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float TimerMax = 2f;
                    if (lastTimePlayed + TimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            default:
                return true;
                //return SoundEffectDelay(sound, 2f);
        }
    }
    //

    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAsset.SoundAudioClip soundAudioClip in SoundAsset.Instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + "not found!");
        return null;
    }

    public static float GetVolume(Sound sound)
    {
        foreach (SoundAsset.SoundAudioClip soundAudioClip in SoundAsset.Instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.volume;
            }
        }
        Debug.LogError("Volume " + sound + "not found!");
        return 0;
    }

    public static AudioMixerGroup GetAudioMixerGroup(Sound sound)
    {
        foreach (SoundAsset.SoundAudioClip soundAudioClip in SoundAsset.Instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioMixerGroup;
            }
        }
        Debug.LogError("AudioMixerGroup " + sound + "not found!");
        return null;
    }
}

