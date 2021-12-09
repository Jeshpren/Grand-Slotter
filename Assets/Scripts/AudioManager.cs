using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }


    void Start()
    {
        //* play atmosphere sound at random start time
        Play("Atmosphere", UnityEngine.Random.Range(0f, 180f));
    }




    public void Play(string name, float time)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        s.source.time = time;
    }

    public void PlayDelayed(string name, float delay, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayDelayed(delay);
        s.source.pitch = pitch;
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }


}
