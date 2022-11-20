using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suj
{
    public class SoundManager : MonoBehaviour
    {
        #region Singleton
        private static SoundManager Instance;

        public static SoundManager instance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = FindObjectOfType<SoundManager>();
                }
                return Instance;
            }
        }
        #endregion

        private AudioSource bgm;
        private AudioSource eff;

        public float masterVolumeBGM = 1f;
        public float masterVolumeEFF = 1f;


        [SerializeField]
        private AudioClip[] charBgmAudioClips;

        Dictionary<string, AudioClip> bgmAudioClipsDic = new Dictionary<string, AudioClip>(); //bgm ��ųʸ�


        [SerializeField]
        private AudioClip[] effAudioClips;

        Dictionary<string, AudioClip> effAudioClipsDic = new Dictionary<string, AudioClip>(); //ȿ���� ��ųʸ�

        private void Awake()
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);

            bgm = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>();
            eff = GameObject.Find("EFFSoundPlayer").GetComponent<AudioSource>();

            foreach (AudioClip audioclip in charBgmAudioClips)
            {
                bgmAudioClipsDic.Add(audioclip.name, audioclip);
            }
            foreach (AudioClip audioclip in effAudioClips)
            {
                effAudioClipsDic.Add(audioclip.name, audioclip);
            }
        }

        //SoundManager.instance.PlayEffSound("ȿ���������̸�");
        public void PlayEffSound(string name)
        {
            if (effAudioClipsDic.ContainsKey(name) == false)
            {
                Debug.Log(name + " is not Contained audioClipsDic");
                return;
            }
            eff.PlayOneShot(effAudioClipsDic[name]);
        }

        //SoundManager.instance.PlayBGMSound("BGM�����̸�");
        public void PlayBGMSound(string name)
        {
            bgm.loop = true;
            bgm.clip = bgmAudioClipsDic[name];
            bgm.Play();
        }

        public void SoundOff()
        {
            bgm.Stop();
        }


        public void SetBgmVolume(float fVolume)
        {
            switch (fVolume)
            {
                case 1:
                    bgm.mute = true;
                    break;
                case 0:
                    bgm.mute = false;
                    break;
            }
        }

        public void SetEffVolume(float fVolume)
        {
            switch (fVolume)
            {
                case 1:
                    eff.mute = true;
                    break;
                case 0:
                    eff.mute = false;
                    break;
            }
        }
    }
}

