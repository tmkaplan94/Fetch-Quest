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


    public void LoadMultiplayerScene()
    {
        SceneManager.LoadScene(MultiplayerScene);
    }

    public void LoadSingleplayerScene()
    {
        SceneManager.LoadScene(SingleplayerScene);
    }
    
    public void Quit()
    {
        Debug.Log("Application.Quit()");
        Application.Quit();
    }

}