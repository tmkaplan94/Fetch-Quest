/*
 * Author: Loc Trinh
 * Contributors: Epitome
 * Summary: Manages different audio clips throughout scenes.
 *
 * Description
 * - To use Music: AudioManager.Instance.PlayMusic(<name>);
 * - To use SFX: AudioManager.Instance.PlaySFX(<name>);
 * - To use Cross Fade: AudioManager.Instance.PlayMusicWithCrossFade(<name>, float)
 * - Music Names: ["Life_of_a_Pet.mp3"]
 * - SFX Names: ["Crowd_Background.mp3", 
                 "Cursor_Click_SFX.mp3", 
                 "Cursor_Hover_SFX.mp3", 
                 "Pick_Up_SFX.mp3", 
                 "Score_Up_SFX.mp3"]
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
    public Sounds[] _sounds;
    private AudioSource currentMusic;
    private AudioSource currentSFX;
    [SerializeField] private float musicVolume;
    [SerializeField] private float sfxVolume;
    private bool firstMusicSourceIsActive;
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        // Make sure to enable loop on music sources
        currentMusic.loop = true;
    }

    public void PlaySound(String n)
    {
        for (int i = 0; i < _sounds.Length; i++)
        {
            if (_sounds[i].name == n)
            {
                _sounds[i].Play();
                return;
            }
        }
 
        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list, " + n);
    }

    public void StopSound(string n)
    {
        for (int i = 0; i < _sounds.Length; i++)
        {
            if (_sounds[i].name == n)
            {
                _sounds[i].Stop();
                return;
            }
        }
 
        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list, " + n);
    }

    public void SetMusicVolume(float volume)
    {
        currentMusic.volume = musicVolume;
    }
    public void SetSFXVolume(float volume)
    {
        currentSFX.volume = sfxVolume;
    }
}