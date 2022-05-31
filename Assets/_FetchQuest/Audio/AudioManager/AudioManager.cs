/*
 * Author: Loc Trinh
 * Contributors: Grant Reed
 * Summary: Manages different audio clips throughout scenes.
 *
 * Description
 * - To use Music: AudioManager.Instance.PlayMusic(<name>);
 * - To use SFX: AudioManager.Instance.PlaySFX(<name> or AudioNames.<name>, <pos>);
 *
 * Updates
 * - N/A
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Photon.Pun;

public class AudioManager : MonoBehaviourPun
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
    [SerializeField] private List<AudioClip> _SFXClips = new List<AudioClip>();
    [SerializeField] private AudioSource currentMusic;
    [SerializeField] private AudioSource currentSFX;
    // Volume Sliders
    [SerializeField] public Slider sliderMaster;
    [SerializeField] public Slider sliderMusic;
    [SerializeField] public Slider sliderSFX;
    // Audio Mixer
    [SerializeField] private AudioMixer _mixer;
    // Scriptable Object Reference
    [SerializeField] private BarksSO[] _barks;
    // Var
    private float _sfxVol;
    #endregion
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _mixer.GetFloat("SFX", out _sfxVol);
        
        foreach(var barkSO in _barks)
        {
            _SFXClips.Add(barkSO._barkAudio);
        }
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
        if(PhotonNetwork.IsConnected)
        {
            if(photonView.IsMine)
            {
                PhotonView v = GetComponent<PhotonView>();
                v.RPC("PlaySFXRPC", RpcTarget.All,n,pos);
            }
        }
        else
        {
            PlaySFXRPC(n,pos);
        }

    }
    [PunRPC]
    private void PlaySFXRPC(String n, Vector3 pos)
    {
        foreach(AudioClip _clip in _SFXClips)
        {
            if(_clip.name == n)
            {
                _mixer.GetFloat("SFX", out _sfxVol);
                currentSFX.clip = _clip;
                _mixer.GetFloat("Master", out float _masterVol); // gets master volume log10
                float _masterAdjustment = Mathf.Pow(10, _masterVol/20); // gets master volume into linear
                AudioSource.PlayClipAtPoint(currentSFX.clip, pos, _masterAdjustment * Mathf.Pow(10, _sfxVol/20));
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
        foreach(AudioClip _clip in _SFXClips)
        {
            if(_clip.name == n)
            {
                currentSFX.clip = null;
                return;
            }
        }
 
        // no sound with _name
        Debug.LogWarning("AudioManager: SFX not found in list, " + n);
    }
    #endregion
    #region AudioControl
    // Sets the master volume in the mixer with slider values.
    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterSliderPref", volume);
        _mixer.SetFloat("Master", Mathf.Log10(volume)*20);
    }
    // Sets the music volume in the mixer with slider values.
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicSliderPref", volume);
        _mixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }
    // Sets the SFX volume in the mixer with slider values.
    public void SetSFXVolume(float volume) // 0.0001 - 1.0
    {
        PlayerPrefs.SetFloat("SFXSliderPref", volume);
        _mixer.SetFloat("SFX", Mathf.Log10(volume)*20);
    }
    
    #endregion  
}