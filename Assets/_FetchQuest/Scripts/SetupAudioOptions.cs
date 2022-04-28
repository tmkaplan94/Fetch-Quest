
using UnityEngine;
using UnityEngine.UI;

public class SetupAudioOptions : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Toggle muteSFX;
    [SerializeField] private Toggle muteMaster;
    [SerializeField] private Toggle muteMusic;
    
    

    void Start()
    {
        masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);

        muteSFX.onValueChanged.AddListener(AudioManager.Instance.MuteSFX);
        muteMaster.onValueChanged.AddListener(AudioManager.Instance.MuteMaster);
        muteMusic.onValueChanged.AddListener(AudioManager.Instance.MuteMusic);

        AudioManager.Instance.muteMaster = muteMaster;
        AudioManager.Instance.muteMusic = muteMusic;
        AudioManager.Instance.muteSFX = muteSFX;
        AudioManager.Instance.sliderMaster = masterSlider;
        AudioManager.Instance.sliderMusic = musicSlider;
        AudioManager.Instance.sliderSFX = SFXSlider;

    }

}
