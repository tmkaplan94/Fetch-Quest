/*
 * Author: Tyler Kaplan
 * Contributors:
 * Summary: Handles the pause menu functionality.
 *
 * Description
 * - Game will pause and resume on ESC, utilizing timescale
 * - displays and hides pause menu on pause and resume
 *
 * Updates
 * - Tyler 4/27/22: now calls Resume() on Quit() calls to reset timescale
 */

using System;
using Photon.Pun;
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
        Resume();
        bool networked = false;
        try
        {
            networked = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().IsNetworked;
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        
        if (networked)
        {
            PhotonNetwork.Disconnect();
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        Destroy(AudioManager.Instance.gameObject);
    }
    
    #endregion
    
}