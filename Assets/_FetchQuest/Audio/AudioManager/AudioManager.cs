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
    public AudioSource[] musicSources;
    private AudioSource currentMusic;
    public AudioSource[] sfxSource;
    private AudioSource currentSFX;
    [SerializeField] private float musicVolume = 1.0f;
    private bool firstMusicSourceIsActive;
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        // Make sure to enable loop on music sources
        currentMusic.loop = true;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        foreach(AudioSource _source in musicSources)
        {
            if(_source.clip == musicClip)
            {
                currentMusic = _source;
                _source.Play();
                break;
            }
        }
    }
    // GET BACK TO THIS LATER. TOO TIRED RN [3:29AM]
    /*
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        // Determine which source is active
        AudioSource activeSource = (firstMusicSourceIsActive)? currentMusic;
        AudioSource newSource = (firstMusicSourceIsActive) ? musicSource2 : musicSource;

        // Swap the source
        firstMusicSourceIsActive = !firstMusicSourceIsActive;

        // Set the fields of the audio source, then start the coroutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, musicClip, transitionTime));
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, AudioClip music, float transitionTime)
    {
        // Make sure the source is active and playing
        if (!original.isPlaying)
            original.Play();

        newSource.Stop();
        newSource.clip = music;
        newSource.Play();

        float t = 0.0f;

        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        {
            original.volume = (musicVolume - ((t / transitionTime) * musicVolume));
            newSource.volume = (t / transitionTime) * musicVolume;
            yield return null;
        }

        // Make sure we don't end up with a weird float value
        original.volume = 0;
        newSource.volume = musicVolume;

        original.Stop();
    }*/

    public void PlaySFX(AudioClip clip)
    {
        foreach(AudioSource _source in sfxSource)
        {
            if(_source.clip == clip)
            {
                currentSFX = _source;
                _source.Play();
                break;
            }
        }
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        foreach(AudioSource _source in sfxSource)
        {
            if(_source.clip == clip)
            {
                _source.volume = volume;
                currentSFX = _source;
                _source.Play();
                break;
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        currentMusic.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        currentSFX.volume = volume; // prolly bug
    }
}