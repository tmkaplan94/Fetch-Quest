/*
 * Author: Tyler Kaplan
 * Contributors: 
 * Summary: not implemented yet
 *
 * Description
 * - 
 */

using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogSelectionManager : MonoBehaviour
{
    #region Serialized Private Fields

    [SerializeField] private Transform audioPos;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject waitingText;
    [SerializeField] private Players players;

    #endregion
    
    #region Private Fields

    private bool _isNetworked;
    private int _viewID;
    
    #endregion


    #region MonoBehavior Callbacks

    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
        {
            _isNetworked = false;
        }
        else
        {
            _viewID = FindObjectOfType<NetworkManager>().ID;
            _isNetworked = true;
        }
    }

    #endregion


    #region Public Methods

    public void SelectDog()
    {
        if (_isNetworked)
        {
            players.ImReady(_viewID);
            Debug.Log("Player" + _viewID + " is ready to play");
            selectButton.SetActive(false);
            waitingText.SetActive(true);
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
            SceneManager.LoadScene("Grant");
        }

        if (players.AllAreReadyToPlay())
        {
            Debug.Log("All " + players.Length() + " players are ready to play");
            AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
            PhotonNetwork.LoadLevel("Grant");
        }
    }
    
    #endregion
}