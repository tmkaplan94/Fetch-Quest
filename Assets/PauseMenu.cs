using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Private Serialized Fields

    [SerializeField] private GameObject UI;
    
    #endregion


    #region Private Fields

    private bool _gameIsPaused;
    
    #endregion


    #region MonoBehavior Callbacks

    private void Start()
    {
        _gameIsPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    #endregion


    #region Public Methods

    public void Resume()
    {
        UI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }

    public void Pause()
    {  
        UI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Title");
    }
    
    #endregion
    
}