using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public static class SoundManager
{
    #region Variables
    #endregion

    public static void Play(AudioClip clip, float volume)
    {
        GameObject sound = new GameObject("Sound");

        AudioSource source = sound.AddComponent<AudioSource>();
        source.volume = volume;
        source.PlayOneShot(clip);
    }
}
