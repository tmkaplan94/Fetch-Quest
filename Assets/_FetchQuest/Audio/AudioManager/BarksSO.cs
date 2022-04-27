using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Barks", menuName = "ScriptableObject/Barks")]
public class BarksSO : ScriptableObject
{
    public string _dogName;
    public AudioClip _barkAudio;
    public AudioMixerGroup _mixerGroup;
    [Range(0.1f, 1f)] public float _pitch;
    [Range(0.0001f, 1f)] public float _volume;
    
}
