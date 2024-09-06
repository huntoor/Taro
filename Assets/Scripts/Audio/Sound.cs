using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string clipName;

    public AudioClip clip;

    [Range(0, 2)] public float volume;
    [Range(0, 2)] public float pitch;
    public bool pitchShifter;
    public bool loop;


    [HideInInspector] public AudioSource source;
}
