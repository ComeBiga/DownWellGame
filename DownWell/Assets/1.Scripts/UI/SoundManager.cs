using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public int vBGM = 1;
    public int vEff = 1;

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

    public void muteBGM()
    {
        if (vBGM == 1)  //�Ҹ� ���� �� Ŭ���ϸ�
        {
            bgm.volume = 0;  //�Ҹ�����
            vBGM = 0;
        }
        else if (vBGM == 0) //�Ҹ� ���� �� Ŭ���ϸ�
        {
            bgm.volume = PlayerPrefs.GetFloat("BgmVolume");
            bgm.Play();  //��� �����
            vBGM = 1;
        }
        PlayerPrefs.SetInt("volBGM", vBGM);
    }
    public void muteEff()
    {
        if (vEff == 1)
        {
            eff.volume = 0;
            vEff = 0;
        }
        else if (vEff == 0)
        {
            eff.volume = PlayerPrefs.GetFloat("EffectVolume");
            vEff = 1;
        }
        PlayerPrefs.SetInt("volEff", vEff);
    }

    public void SetBgmVolume(float fVolume)
    {
        masterVolumeBGM = fVolume;
        vBGM = 1;
        bgm.volume = fVolume * masterVolumeBGM;
    }

    public void SetEffVolume(float fVolume)
    {
        masterVolumeEFF = fVolume;
        vEff = 1;
        eff.volume = fVolume * masterVolumeEFF;
    }
}

