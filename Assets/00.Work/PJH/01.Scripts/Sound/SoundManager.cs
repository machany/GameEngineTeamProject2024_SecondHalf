using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESFXName
{
    Click,
    Hover
}

public class SoundManager : MonoSingleton<SoundManager>
{
    [Header("BGM")] [SerializeField] private List<AudioClip> bgmClips;
    [SerializeField] private float bgmVolume;

    [Header("SFX")] [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private float sfxVolume;

    [SerializeField] private int channel;

    private AudioSource _bgmPlayer;
    private AudioSource[] _sfxPlayer;

    private int _channelIndex;

    public float BgmVolume
    {
        get => bgmVolume;

        set
        {
            bgmVolume = value;
            _bgmPlayer.volume = bgmVolume;
        }
    }

    public float SfxVolume
    {
        get => sfxVolume;

        set
        {
            sfxVolume = value;

            foreach (AudioSource item in _sfxPlayer)
                item.volume = sfxVolume;
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        // BGM 플레이어 초기화
        GameObject bgmObj = new GameObject("BGMPlayer");
        bgmObj.transform.parent = transform;

        _bgmPlayer = bgmObj.AddComponent<AudioSource>();
        _bgmPlayer.playOnAwake = false;
        _bgmPlayer.loop = true;
        _bgmPlayer.volume = bgmVolume;
        _bgmPlayer.clip = bgmClips[0];

        // SFX 플레이어 초기화
        GameObject sfxObj = new GameObject("SFXPlayer");
        sfxObj.transform.parent = transform;
        _sfxPlayer = new AudioSource[channel];

        for (int i = 0; i < _sfxPlayer.Length; i++)
        {
            _sfxPlayer[i] = sfxObj.AddComponent<AudioSource>();
            _sfxPlayer[i].playOnAwake = false;
            _sfxPlayer[i].volume = sfxVolume;
        }
    }

    public void PlayBgm(int value)
    {
        _bgmPlayer.clip = bgmClips[value];
        _bgmPlayer.Play();
    }

    public void StopBgm(int value)
    {
        _bgmPlayer.clip = bgmClips[value];
        _bgmPlayer.Stop();
    }
    
    //효과음 재생(AudioManager.Instance.PlaySfx(AudioManager.Sfx.실행할 효과음); 형태로 사용)
    public void PlaySfx(ESFXName soundName)
    {
        for (int i = 0; i < _sfxPlayer.Length; i++)
        {
            int loopIndex = (i + _channelIndex) % _sfxPlayer.Length;

            if (_sfxPlayer[loopIndex].isPlaying)
                continue;
            
            _channelIndex = loopIndex;
            _sfxPlayer[loopIndex].clip = sfxClips[(int)soundName];
            _sfxPlayer[loopIndex].Play();
            break;
        }
    }
}