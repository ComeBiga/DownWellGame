using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Comebiga
{
    public class SoundManager : MonoBehaviour
    {

        public Sound[] sounds;

        public static SoundManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            foreach(Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void Play(string name)
        {
            Sound s = System.Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            s.source.Play();
        }

        public void Play(Sound s)
        {
            s.source.Play();
        }

        public void Stop(string name)
        {
            Sound s = System.Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Stop();
        }

        public void Stop(Sound.SoundType type)
        {
            Sound[] s = System.Array.FindAll(sounds, sound => sound.type == type);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            foreach (var sound in s)
            {
                sound.source.Stop();
            }
        }

        public void Mute(Sound.SoundType type, bool value)
        {
            Sound[] s = System.Array.FindAll(sounds, sound => sound.type == type);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            foreach(var sound in s)
            {
                sound.source.mute = value;
            }
        }
    }
}
