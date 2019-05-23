using UnityEngine;
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
    public float volume, pitch;
    #endregion
}

public class AudioManager : MonoBehaviour
{
    #region Variables
    public Sound[] sounds;
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

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            if (Debug.isDebugBuild) { print($"Sound: {name} not found."); }
            return;
        }

        s.source.Play();
    }
}
