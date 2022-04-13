using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    
    [Tooltip("The name of the scene to load when the Multiplayer Button is selected.")]
    [SerializeField] private string MultiplayerScene;

    [Tooltip("The name of the scene to load when Singleplayer Button is selected.")]
    [SerializeField] private string SingleplayerScene;

    [Tooltip("The name of the scene to load when Testing Button is selected. Used in development only.")]
    [SerializeField] private string TestingScene;

    
    public void LoadMultiplayerScene()
    {
        SceneManager.LoadScene(MultiplayerScene);
    }

    public void LoadSingleplayerScene()
    {
        SceneManager.LoadScene(SingleplayerScene);
    }

    public void LoadTestingScene()
    {
        SceneManager.LoadScene(TestingScene);
    }
    
}