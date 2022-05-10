/*
 * Author: https://forum.unity.com/threads/audio-mixer-group-in-code.461712/
 * Contributors: Loc Trinh
 * Summary: Serializable audio file for easy design configurations.
 *
 * Description
 * - Just add in audio clips and start editing.
 *
 * Updates
 * - N/A
 */

using UnityEngine;
using UnityEngine.Audio;
 
[System.Serializable]
public class Sounds : MonoBehaviour
{
    public string _name;
    public AudioClip clip;
    public AudioMixerGroup audioMixer;
 
    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;
 
    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;
 
    public bool loop = false;
 
    private AudioSource source;
 
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixer;
    }
 
    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }
 
    public void Stop()
    {
        source.Stop();
    }
 
}