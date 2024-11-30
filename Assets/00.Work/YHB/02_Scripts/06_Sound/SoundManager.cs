using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private List<SoundChannelSO> soundChannels;
    public Dictionary<SoundType, SoundChannelSO> _channels;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _channels = new Dictionary<SoundType, SoundChannelSO>();

        foreach (SoundChannelSO soundChannel in soundChannels)
        {
            GameObject obj = new GameObject(soundChannel.name);
            obj.transform.parent = transform;

            soundChannel.clips = new Dictionary<string, AudioClip>();

            foreach (SoundSO sound in soundChannel.sounds)
                soundChannel.clips.Add(sound.key, sound.clip);

            soundChannel.players = new AudioSource[soundChannel.channel];
            for (int i = 0; i < soundChannel.players.Length; i++)
            {
                soundChannel.players[i] = obj.AddComponent<AudioSource>();
                soundChannel.players[i].outputAudioMixerGroup = soundChannel.mixerGroup;
                soundChannel.players[i].playOnAwake = soundChannel.playOnAwake;
                soundChannel.players[i].loop = soundChannel.loop;
                soundChannel.players[i].volume = soundChannel.volume;
                soundChannel.players[i].clip = soundChannel.sounds[0].clip;
            }

            _channels.Add(soundChannel.channelType, soundChannel);
        }
    }

    public void PlaySound(SoundType type, string value)
    {
        AudioSource[] audioSources = _channels[type].players;
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying)
                continue;

            try
            {
                audioSources[i].clip = _channels[type].clips[value];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("Can't found key. key Value :" + value);
                return;
            }
            audioSources[i].Play();
            break;
        }
    }

    public void StopSound(SoundType type, string value)
    {
        AudioSource[] audioSources = _channels[type].players;
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (EqualityComparer<AudioClip>.Default.Equals(audioSources[i].clip, _channels[type].clips[value]))
                continue;

            audioSources[i].clip = null;
            audioSources[i].Stop();
            break;
        }
    }

    public void PlaySoundLoopInChannel(SoundType type, float waitTime = 0f)
        => StartCoroutine(PlaySoundLoopInChannelCoroutine(type, waitTime));
    
    public void StopSoundLoopInChannel(SoundType type)
        => StopCoroutine(PlaySoundLoopInChannelCoroutine(type, 0f));

    private IEnumerator PlaySoundLoopInChannelCoroutine(SoundType type, float waitTime)
    {
        NotOverlapValue<AudioClip> clips = new NotOverlapValue<AudioClip>(_channels[type].clips.Values);
        _channels[type].players[0].loop = false;

        while (true)
        {
            _channels[type].players[0].clip = clips.GetValue();
            _channels[type].players[0].Play();
            yield return new WaitUntil(() => !_channels[type].players[0].isPlaying);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void StopSoundInChannel(SoundType type)
        => _channels[type].players.ToList().ForEach(player => player.Stop());
}