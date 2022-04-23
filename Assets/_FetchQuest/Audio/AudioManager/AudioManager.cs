/*
 * Author: Loc Trinh
 * Contributors: Grant Reed
 * Summary: Manages different audio clips throughout scenes.
 *
 * Description
 * - To use Music: AudioManager.Instance.PlayMusic(<name>);
 * - To use SFX: AudioManager.Instance.PlaySFX(<name>);
 * - Music Names: ["Life_of_a_Pet",
                   "Strolling Along"]
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
using UnityEngine.UI;

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
    // Audio stuff
    [Tooltip("All Music Clips")]
    [SerializeField] private AudioClip[] _musicClips;
    [Tooltip("All SFX Clips")]
    [SerializeField] private AudioClip[] _SFXClips;
    [SerializeField] private AudioSource currentMusic;
    [SerializeField] private AudioSource currentSFX;
    // Mute Toggles
    [SerializeField] private Toggle muteMaster;
    [SerializeField] private Toggle muteMusic;
    [SerializeField] private Toggle muteSFX;
    // Volume Sliders
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    // Audio Mixer
    [SerializeField] private AudioMixer _mixer;
    // Temp
    private float _vol = 1;
    // Bool conditions
    private bool _masterMuteBool = false;
    private bool _musicMuteBool = false;
    private bool _sfxMuteBool = false;
    #endregion
    private void Awake(){DontDestroyOnLoad(this.gameObject);}
    private void Start()
    {
        muteMaster.isOn = false;
        muteMusic.isOn = false;
        muteSFX.isOn = false;
    }
    #region Playing Audio
    // A function that plays the music audio with given name.
    public void PlayMusic(String n)
    {
        for (int i = 0; i < _musicClips.Length; i++)
        {
            if (_musicClips[i].name == n)
            {
                currentMusic.clip = _musicClips[i];
                currentMusic.Play();
                return;
            }
        }
        // no sound with _name
        Debug.LogWarning("AudioManager: Music not found in list, " + n);
    }
    // A function that plays the SFX audio with given name.
    public void PlaySFX(String n, Vector3 pos)
    {
        for (int i = 0; i < _SFXClips.Length; i++)
        {
            if (_SFXClips[i].name == n)
            {
                currentSFX.clip = _SFXClips[i];
                AudioSource.PlayClipAtPoint(currentSFX.clip, pos, _vol);
                return;
            }
        }
        // no sound with _name
        Debug.LogWarning("AudioManager: SFX not found in list, " + n);
    }
    #endregion
    #region StopAudio
    // Optional area meant for special interaction with level design, where
    // you want certain audio group to play only.
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
        for (int i = 0; i < _SFXClips.Length; i++)
        {
            if (_SFXClips[i].name == n)
            {
                _SFXClips[i] = null;
                return;
            }
        }
 
        // no sound with _name
        Debug.LogWarning("AudioManager: SFX not found in list, " + n);
    }
    #endregion
    #region AudioControl
    public void SetMasterVolume(float volume)
    {
        if(!_masterMuteBool)
        {
            _mixer.SetFloat("Master", Mathf.Log10(volume)*20);
        }
    }
    public void SetMusicVolume(float volume)
    {
        if(!_masterMuteBool && !_musicMuteBool)
        {
            _mixer.SetFloat("Music", Mathf.Log10(volume)*20);
        }
    }
    public void SetSFXVolume(float volume) // 0.0001 - 1.0
    {
        if(!_masterMuteBool && !_sfxMuteBool)
        {
            _vol = volume;
            _mixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        }
        
    }
    //bug
    public void MuteMaster(bool _audio)
    {
        _masterMuteBool = _audio;
        if(_audio)
        {
            sliderMaster.interactable = false;
            PlayerPrefs.SetFloat("SavedMasterVol", AudioListener.volume);
            AudioListener.volume = 0f;
        }
        else
        {
            sliderMaster.interactable = true;
            AudioListener.volume = PlayerPrefs.GetFloat("SavedMasterVol");
        }
    }
    public void MuteMusic(bool _audio)
    {
        _musicMuteBool = _audio;
        if(_audio)
        {
            float _currentMusicVol;
            sliderMusic.interactable = false;
            _mixer.GetFloat("Music", out _currentMusicVol);
            PlayerPrefs.SetFloat("SavedMusicVol", _currentMusicVol);
            _mixer.SetFloat("Music", -80f);
        }
        else
        {
            sliderMusic.interactable = true;
            _mixer.SetFloat("Music", PlayerPrefs.GetFloat("SavedMusicVol"));
        }
    }
    public void MuteSFX(bool _audio)
    {
        _sfxMuteBool = _audio;
        if(_audio)
        {
            float _currentSFXVol;
            sliderMusic.interactable = false;
            _mixer.GetFloat("SFX", out _currentSFXVol);
            PlayerPrefs.SetFloat("SavedSFXVol", _currentSFXVol);
            _mixer.SetFloat("SFX", -80f);
        }
        else
        {
            sliderMusic.interactable = true;
            _mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SavedSFXVol"));
        }
    }
    #endregion
}