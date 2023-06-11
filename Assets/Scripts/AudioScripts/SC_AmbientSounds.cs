using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AmbientSounds : MonoBehaviour
{
public float timer;
    public float timerOffset = 30f;

    // Update is called once per frame
    void Update()
    {
        if (timer < Time.time)
        {
            timer = timerOffset + Time.time;

            //Play sound

            var value = UnityEngine.Random.Range(0, 6);

            switch (value)
            {
                case 0:
                    SoundManager.PlaySound(SoundManager.Sound.AmbienceBonus);
                    break; 
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.DelayAmbience);
                    break; 
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.FallingAmbience);
                    break; 
                case 3:
                    SoundManager.PlaySound(SoundManager.Sound.FullStringAmbience);
                    break;
                case 4:
                    SoundManager.PlaySound(SoundManager.Sound.HorrorAmbience);
                    break;
                case 5:
                    SoundManager.PlaySound(SoundManager.Sound.RollingAmbience);
                    break;
                default: break;
            }
        }
    }
}
