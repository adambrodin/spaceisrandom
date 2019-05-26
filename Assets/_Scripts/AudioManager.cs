﻿using UnityEngine;
using UnityEngine.Audio;
using System;
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */
[System.Serializable]
public class Sound
{
    #region Variables
    public string name;
    public bool loop;

    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 2f)]
    public float pitch;
    #endregion
}

public class AudioManager : MonoBehaviour
{
    #region Variables
    public Sound[] sounds;
    [SerializeField]
    private bool globalMute;
    #endregion

    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public Sound GetSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            if (Debug.isDebugBuild) { print($"Sound: {name} not found."); }
            return null;
        }
        return s;
    }

    public void Set(string name, float pitch, float volume)
    {
        Sound sound = GetSound(name);
        if (sound != null)
        {
            if (sound.source.pitch != pitch) sound.source.pitch = pitch;
            if (sound.source.volume != volume) sound.source.volume = volume;
        }
    }

    public void SetPlaying(string name, bool shouldPlay)
    {
        if (shouldPlay && !globalMute) GetSound(name).source.Play();
        if (!shouldPlay) GetSound(name).source.Stop();
    }
}
