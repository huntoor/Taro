using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    public static SoundManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string clipName)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == clipName);

        if (s == null)
        {
            Debug.LogWarning("Sound Name: " + clipName + " Not Found");
            return;
        }

        if (s.pitchShifter)
        {
            s.source.pitch = PitchShifter(s);
        }

        s.source.Play();

    }

    private float PitchShifter(Sound sound)
    {
        float randomPitch = UnityEngine.Random.Range(-0.3f, 0.3f);
        
        return sound.pitch + randomPitch;
    }
}
