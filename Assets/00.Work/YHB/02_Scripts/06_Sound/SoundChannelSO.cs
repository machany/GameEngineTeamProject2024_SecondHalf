using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    BGM,
    SFX,
    GameOver
}

[CreateAssetMenu(menuName = "SO/Sound/SoundChannelSO")]
public class SoundChannelSO : ScriptableObject
{
    public SoundType channelType;
    public AudioMixerGroup mixerGroup;
    [Range(1, sbyte.MaxValue)]
    public sbyte channel = 1;
    public bool loop = false;
    public bool playOnAwake = false;
    [Range(0f, 1f)]
    public float volume;

    public SoundSO[] sounds;
    public Dictionary<string, AudioClip> clips;
    [HideInInspector] public AudioSource[] players;
}
