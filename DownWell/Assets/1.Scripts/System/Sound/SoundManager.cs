using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Comebiga
{
    public enum AudioState { ON = 0, OFF = -80 }

    public class SoundManager : MonoBehaviour
    {

        public static SoundManager instance;
        public AudioMixerGroup bgmMixer;
        public AudioMixerGroup sfxMixer;

        public Sound[] sounds;

        private float bgmVolume;
        private float sfxVolume;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            foreach(Sound s in sounds)
            {
                Init(s);
            }

            UnmuteBGM();
            UnmuteSFX();
        }

        public void Init(Sound s)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        public void Play(string name)
        {
            Sound s = System.Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            
            SetAudioMixerGroupBySoundType(s);
            s.source.Play();
        }

        public void Play(Sound s)
        {
            SetAudioMixerGroupBySoundType(s);
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

        public void Stop(Sound _s)
        {
            Sound s = System.Array.Find(sounds, sound => sound == _s);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + _s.name + " not found!");
                return;
            }

            s.source.Stop();
        }

        public void Stop(Sound.SoundType type)
        {
            Sound[] s = System.Array.FindAll(sounds, sound => sound.type == type);
            if (s == null)
            {
                Debug.LogWarning(type.ToString() + " sound not found!");
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

        public void MuteBGM()
        {
            var parameter = "bgmVolume";
            bgmMixer.audioMixer.SetFloat(parameter, (float)AudioState.OFF);
        }

        public void UnmuteBGM()
        {
            bgmMixer.audioMixer.SetFloat("bgmVolume", (float)AudioState.ON);
        }
        
        public void MuteSFX()
        {
            var parameter = "sfxVolume";
            sfxMixer.audioMixer.SetFloat(parameter, (float)AudioState.OFF);
        }

        public void UnmuteSFX()
        {
            sfxMixer.audioMixer.SetFloat("sfxVolume", (float)AudioState.ON);
        }

        private void SetAudioMixerGroupBySoundType(Sound s)
        {
            switch(s.type)
            {
                case Sound.SoundType.BACKGROUND:
                    s.source.outputAudioMixerGroup = bgmMixer;
                    break;
                case Sound.SoundType.EFFECT:
                    s.source.outputAudioMixerGroup = sfxMixer;
                    break;
            }
        }
    }
}
