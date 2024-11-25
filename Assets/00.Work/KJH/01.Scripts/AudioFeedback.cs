using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedback : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;
    [Range(0,1)]
    public float volume = 1.0f;

    public void PlayClip()
    {
        if (_audioSource == null)
            return;
        
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(_audioClip);
    }
}
