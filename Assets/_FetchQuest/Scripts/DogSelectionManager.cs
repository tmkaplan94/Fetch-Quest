/*
 * Author: Tyler Kaplan
 * Contributors: 
 * Summary: not implemented yet
 *
 * Description
 * - 
 */

using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogSelectionManager : MonoBehaviourPunCallbacks
{
    
    #region Serialized Private Fields

    [SerializeField] private Transform audioPos;

    [SerializeField] private GameObject singleplayerPanel;
    [SerializeField] private GameObject hostPanel;
    [SerializeField] private GameObject guestPanel;

    #endregion
    
    
    #region Private Fields

    private GameObject _rotationManager;
    private bool _isNetworked;
    private bool _guestIsReady;
    private bool _hostIsReady;

    #endregion

    
    #region MonoBehavior Callbacks

    private void Awake()
    {
        _rotationManager = GameObject.Find("RotationManager");
        
        // if the game is not networked
        if (FindObjectOfType<NetworkManager>() == null)
        {
            _isNetworked = false;
            SetupSingleplayerPanel();
        }
        // if the game is networked
        else
        {
            _isNetworked = true;
            _guestIsReady = false;
            _hostIsReady = false;
            
            // if player is the host
            if (PhotonNetwork.IsMasterClient)
            {
                SetupHostPanel();
            }
            // if player is the guest
            else
            {
                SetupGuestPanel();
            }
        }
    }

    #endregion

    
    #region Public Methods

    public void SingleplayerSelectDog()
    {
        // play click sound
        AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
        
        // load scene for singleplayer
        SceneManager.LoadScene("Grant");
    }
    
    public void GuestSelectDog()
    {
        // play click sound
        AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
        
        // notify that guest player is ready to play
        photonView.RPC("GuestIsReady", RpcTarget.All);
        _rotationManager.GetComponent<RotationManager>().HasSelected = true;

        // enable/disable proper UI elements
        guestPanel.transform.GetChild(0).gameObject.SetActive(false);
        guestPanel.transform.GetChild(1).gameObject.SetActive(true);
    }
    
    public void HostSelectDog()
    {
        // play click sound
        AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);

        // host is now ready
        _hostIsReady = true;
        _rotationManager.GetComponent<RotationManager>().HasSelected = true;
        
        // enable/disable proper UI elements
        hostPanel.transform.GetChild(0).gameObject.SetActive(false);
        if (_guestIsReady)
        {
            hostPanel.transform.GetChild(1).gameObject.SetActive(false);
            hostPanel.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            hostPanel.transform.GetChild(1).gameObject.SetActive(true);
            hostPanel.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void HostStartGame()
    {
        // play click sound
        AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
        
        // load scene using photon
        PhotonNetwork.LoadLevel("Grant");
    }
    
    public void GoBack(){
        
        // play click sound
        AudioManager.Instance.PlaySFX(AudioNames.click, audioPos.position);
        
        // if game is networked, go back to lobby scene
        if (_isNetworked)
        {
            PhotonNetwork.LoadLevel("Lobby");
        }
        // otherwise, go back to the main menu
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    
    #endregion


    #region Private Methods

    private void SetupSingleplayerPanel()
    {
        singleplayerPanel.SetActive(true);
        singleplayerPanel.transform.GetChild(0).gameObject.SetActive(true);

        hostPanel.SetActive(false);
        guestPanel.SetActive(false);
    }

    private void SetupHostPanel()
    {
        hostPanel.SetActive(true);
        hostPanel.transform.GetChild(0).gameObject.SetActive(true);
        hostPanel.transform.GetChild(1).gameObject.SetActive(false);
        hostPanel.transform.GetChild(2).gameObject.SetActive(false);
        
        guestPanel.SetActive(false);
        singleplayerPanel.SetActive(false);
    }

    private void SetupGuestPanel()
    {
        guestPanel.SetActive(true);
        guestPanel.transform.GetChild(0).gameObject.SetActive(true);
        guestPanel.transform.GetChild(1).gameObject.SetActive(false);
        
        hostPanel.SetActive(false);
        singleplayerPanel.SetActive(false);
    }

    [PunRPC]
    private void GuestIsReady()
    {
        _guestIsReady = true;
        
        if (PhotonNetwork.IsMasterClient && _hostIsReady)
        {
            // enable the play button for the host
            hostPanel.transform.GetChild(0).gameObject.SetActive(false);
            hostPanel.transform.GetChild(1).gameObject.SetActive(false);
            hostPanel.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    #endregion
    
}