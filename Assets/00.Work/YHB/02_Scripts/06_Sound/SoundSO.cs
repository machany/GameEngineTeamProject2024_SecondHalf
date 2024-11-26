using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Sound/SoundSO")]
public class SoundSO : ScriptableObject
{
    public string key;
    public AudioClip clip;

    private void OnValidate()
    {
        key = this.name;
    }
}
