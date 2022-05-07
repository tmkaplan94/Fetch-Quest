/*
 * Author: Tyler Kaplan
 * Contributors: 
 * Summary: Deals with scene transitioning from Title scene
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    
    [Tooltip("The name of the scene to load when the Multiplayer Button is selected.")]
    [SerializeField] private string MultiplayerScene;

    [Tooltip("The name of the scene to load when Singleplayer Button is selected.")]
    [SerializeField] private string SingleplayerScene;

    [SerializeField] private GameObject audioCanvas;
    [SerializeField] private GameObject mainUICanvas;
    [SerializeField] private Transform centerScene;


    public void LoadMultiplayerScene()
    {
        SceneManager.LoadScene(MultiplayerScene);
    }

    public void LoadSingleplayerScene()
    {
        AudioManager.Instance.PlaySFX(AudioNames.click, centerScene.position);
        SceneManager.LoadScene(SingleplayerScene);
    }
    
    public void Quit()
    {
        AudioManager.Instance.PlaySFX(AudioNames.click, centerScene.position);
        Debug.Log("Application.Quit()");
        Application.Quit();
    }

    public void PlayHoverAudio()
    {
        AudioManager.Instance.PlaySFX(AudioNames.hover, centerScene.position);
    }

    public void SetAudioOptionsVisibility(){
        if(audioCanvas.activeSelf){
            audioCanvas.SetActive(false);
        }else{
            audioCanvas.SetActive(true);
        }

        if(mainUICanvas.activeSelf){
            mainUICanvas.SetActive(false);
        }else{
            mainUICanvas.SetActive(true);
        }
    }

}