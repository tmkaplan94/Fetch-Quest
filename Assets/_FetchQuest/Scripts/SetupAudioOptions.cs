
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SetupAudioOptions : MonoBehaviourPun
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    void Start()
    {
        masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        
        AudioManager.Instance.sliderMaster = masterSlider;
        AudioManager.Instance.sliderMusic = musicSlider;
        AudioManager.Instance.sliderSFX = SFXSlider;

        masterSlider.value = PlayerPrefs.GetFloat("MasterSliderPref");
        musicSlider.value = PlayerPrefs.GetFloat("MusicSliderPref");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXSliderPref");
    }
}
