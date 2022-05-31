using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    
    #region Private Serialized Fields

    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private InputField playerName;
    [SerializeField] private InputField roomName;
    [SerializeField] private GameObject createButton;
    [SerializeField] private GameObject joinButton;
    [SerializeField] private GameObject connectingText;

    [SerializeField] private GameObject launcherPanel;
    [SerializeField] private GameObject roomListingsPanel;
    [SerializeField] private GameObject roomPanel;
    
    [SerializeField] private Transform roomsContent;
    [SerializeField] private RoomListing roomListing;
    [SerializeField] private Transform playersContent;
    [SerializeField] private PlayerListing playerListing;

    #endregion


    #region Private Fields

    private RoomOptions _roomOptions = new RoomOptions();
    private List<string> _availableRooms = new List<string>();
    private List<PlayerListing> _playerListings = new List<PlayerListing>();

    #endregion


    #region Properties

    public bool IsNetworked { get; private set; }

    #endregion


    #region MonoBehavior Callbacks

    private void Awake()
    {
        // game is networked if this object exists
        IsNetworked = true;
        
        // persist in scenes
        DontDestroyOnLoad(this);

        // find and destroy duplicates
        GameObject[] duplicateNetworkManagers = GameObject.FindGameObjectsWithTag("NetworkManager");
        foreach (GameObject networkManager in duplicateNetworkManagers)
        {
            // if duplicate is not me, destroy it
            if (gameObject != networkManager.gameObject)
            {
                Destroy(networkManager);
            }
        }

        // all clients in the same room will automatically sync level
        PhotonNetwork.AutomaticallySyncScene = true;
        
        // connect to Photon servers
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameSettings.GameVersion;
        }

        // create room options
        _roomOptions.MaxPlayers = gameSettings.MaxPlayers;
    }

    #endregion


    #region MonoBahaviorPunCallbacks Callbacks
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        
        // connect to lobby to see room updates
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        // enable player to create/join rooms
        connectingText.SetActive(false);
        createButton.SetActive(true);
        joinButton.SetActive(true);
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
        IsNetworked = false;
        Destroy(AudioManager.Instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarningFormat("Room creation failed because {0}", message);
    }

    public override void OnJoinedRoom()
    {
        SetUpRoomPanel();
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room list updated");
        
        _availableRooms.Clear();
        
        foreach (RoomInfo roomInfo in roomList)
        {
            _availableRooms.Add(roomInfo.Name);
            RoomListing room = Instantiate(roomListing, roomsContent);
            room.SetInformation(roomInfo);
        }
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    
    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    #endregion
    

    #region Public Methods

    public void CreateRoom()
    {
        // check if connected to Photon servers
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogWarning("Cannot attempt to create room, not connected to Photon servers");
            return;
        }
        
        // set player name
        string enteredNameValue = playerName.text;
        if (enteredNameValue == "")
        {
            playerName.text = gameSettings.DefaultPlayerName + GetRandomNumber();
        }
        
        // check room name
        string enteredRoomValue = roomName.text;
        if (enteredRoomValue == "")
        {
            Debug.LogWarning("No room name entered, creating a random room now");
            roomName.text = gameSettings.DefaultRoomName + GetRandomNumber();
        }
        if (_availableRooms.Contains(enteredRoomValue))
        {
            Debug.LogWarning("Room already exists, joining it now...");
            PhotonNetwork.JoinRoom(roomName.text);
            return;
        }

        // create room
        PhotonNetwork.CreateRoom(roomName.text, _roomOptions, TypedLobby.Default);
    }

    public void JoinRoom()
    {
        // check if connected to Photon servers
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogWarning("Cannot attempt to join room, not connected to Photon servers");
            return;
        }
        
        // set player name
        string enteredNameValue = playerName.text;
        if (enteredNameValue == "")
        {
            playerName.text = gameSettings.DefaultPlayerName + GetRandomNumber();
        }
        
        // check room name
        string enteredRoomValue = roomName.text;
        if (enteredRoomValue == "")
        {
            Debug.LogWarning("Please enter a room name");
            return;
        }
        if (!_availableRooms.Contains(enteredRoomValue))
        {
            Debug.LogWarning("Room does not exist, creating it now...");
            PhotonNetwork.CreateRoom(roomName.text, _roomOptions, TypedLobby.Default);
            return;
        }
        
        // join room
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public void LoadDogSelectionScene()
    {
        PhotonNetwork.LoadLevel("DogSelection");
    }

    public void GoToMainMenu()
    {
        Destroy(AudioManager.Instance.gameObject);
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.Disconnect();
    }

    #endregion

    
    #region Private Methods

    private int GetRandomNumber()
    {
        return Random.Range(0, 10000);
    }
    
    private void UpdatePlayerList()
    {
        foreach(PlayerListing player in _playerListings)
        {
            Destroy(player.gameObject);
        }
        _playerListings.Clear();

        if (PhotonNetwork.CurrentRoom == null)
            return;
        
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerListing newPlayer =  Instantiate(playerListing, playersContent);
            newPlayer.SetInformation(player.Value);
            _playerListings.Add(newPlayer);
        }
    }

    private void SetUpRoomPanel()
    {
        // disable or enable proper panels
        launcherPanel.SetActive(false);
        roomListingsPanel.SetActive(false);
        roomPanel.SetActive(true);
        
        // update room name
        roomPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = roomName.text;
        
        // enable proper panels depending on if user is the host or not
        if (PhotonNetwork.IsMasterClient)
        {
            roomPanel.transform.GetChild(2).gameObject.SetActive(true);
            roomPanel.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            roomPanel.transform.GetChild(2).gameObject.SetActive(false);
            roomPanel.transform.GetChild(3).gameObject.SetActive(true);
        }
        
    }

    #endregion

}