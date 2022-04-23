/*
 * Author: Loc Trinh
 * Contributors: Epitome
 * Summary: Manages different audio clips throughout scenes.
 *
 * Description
 * - To use Music: AudioManager.Instance.PlayMusic(<name>);
 * - To use SFX: AudioManager.Instance.PlaySFX(<name>);
 * - Music Names: ["Life_of_a_Pet"]
 * - SFX Names: ["Crowd_Background", 
                 "Cursor_Click_SFX", 
                 "Cursor_Hover_SFX", 
                 "Pick_Up_SFX", 
                 "Score_Up_SFX"]
 *
 * Updates
 * - N/A
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Instance
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    #region Fields
    [Tooltip("All Music Clips")]
    [SerializeField] private AudioClip[] _musicClips;
    [Tooltip("All SFX Clips")]
    [SerializeField] private AudioClip[] _sfxClips;
    [SerializeField] private AudioSource currentMusic;
    [SerializeField] private AudioSource currentSFX;
    private bool firstMusicSourceIsActive;
    #endregion
    private void Awake(){DontDestroyOnLoad(this.gameObject);}
    #region Playing Audio

    public void PlayMusic(String n)
    {
        for (int i = 0; i < _musicClips.Length; i++)
        {
            if (_musicClips[i].name == n)
            {
                Debug.Log("NAME: " + _musicClips[i].name);
                currentMusic.clip = _musicClips[i];
                currentMusic.Play();
                return;
            }
            else{
                Debug.Log("INCORRECT NAME");
            }
        }
 
        // no sound with _name
        Debug.LogWarning("AudioManager: Music not found in list, " + n);
    }

    public void PlaySFX(String n)
    {
        for (int i = 0; i < _sfxClips.Length; i++)
        {
            if (_sfxClips[i].name == n)
            {
                Debug.Log("NAME: " + _sfxClips[i].name);
                currentSFX.clip = _sfxClips[i];
                currentSFX.Play();
                return;
            }
            else{
                Debug.Log("INCORRECT NAME");
            }
        }
 
        // no sound with _name
        Debug.LogWarning("AudioManager: SFX not found in list, " + n);
    }
    #endregion
    #region StopAudio
    public void StopMusic(string n)
    {
        for (int i = 0; i < _musicClips.Length; i++)
        {
            if (_musicClips[i].name == n)
            {
                currentMusic.clip = null;
                return;
            }
        }
 
        // no sound with _name
        Debug.LogWarning("AudioManager: Music not found in list, " + n);
    }

    public void StopSFX(string n)
    {
        for (int i = 0; i < _sfxClips.Length; i++)
        {
            if (_sfxClips[i].name == n)
            {
                _sfxClips[i] = null;
                return;
            }
        }
 
        // no sound with _name
        Debug.LogWarning("AudioManager: SFX not found in list, " + n);
    }
    #endregion
    #region AudioControl
    public void SetMusicVolume(float volume)
    {
        currentMusic.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        currentSFX.volume = volume;
    }
    #endregion
}