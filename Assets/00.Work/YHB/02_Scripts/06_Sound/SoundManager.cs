using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private List<SoundChannelSO> soundChannels;
    public Dictionary<SoundType, SoundChannelSO> channels;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (SoundChannelSO soundChannel in soundChannels)
        {
            GameObject obj = new GameObject(soundChannel.name);
            obj.transform.parent = transform;

            foreach (SoundSO sound in soundChannel.sounds)
                soundChannel.clips.Add(sound.key, sound.clip);

            soundChannel.players = new AudioSource[soundChannel.channel];
            for (int i = 0; i < soundChannel.players.Length; i++)
            {
                soundChannel.players[i] = obj.AddComponent<AudioSource>();
                soundChannel.players[i].playOnAwake = soundChannel.playOnAwake;
                soundChannel.players[i].loop = soundChannel.loop;
                soundChannel.players[i].volume = soundChannel.volume;
                soundChannel.players[i].clip = soundChannel.sounds[0].clip;
            }

            channels.Add(soundChannel.channelType, soundChannel);
        }
    }

    public void PlaySound(SoundType type, string value)
    {
        AudioSource[] audioSources = channels[type].players;
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying)
                continue;

            audioSources[i].clip = channels[type].clips[value];
            audioSources[i].Play();
            break;
        }
    }

    public void StopSound(SoundType type, string value)
    {
        AudioSource[] audioSources = channels[type].players;
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (EqualityComparer<AudioClip>.Default.Equals(audioSources[i].clip, channels[type].clips[value]))
                continue;

            audioSources[i].clip = null;
            audioSources[i].Stop();
            break;
        }
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        SoundType[] list = new SoundType[soundChannels.Count];
        int i = 0;

        foreach (SoundChannelSO soundChannel in soundChannels)
        {
            if (list.Contains(soundChannel.channelType))
            {
                Debug.LogError("Warring!! SoundType of the currently entered soundChannel overlaps!");
                return;
            }
            else
                list[i++] = soundChannel.channelType;

            List<string> keies = new List<string>();
            for (int j = 0; j < soundChannel.sounds.Length; j++)
            {
                if (soundChannel.sounds[j])
                {
                    Debug.LogError("Warring!! key of the currently entered soundChannel'SoundSO's key overlaps!");
                    return;
                }
                keies.Add(soundChannel.sounds[j].key);
            }
        }
    }

#endif
}