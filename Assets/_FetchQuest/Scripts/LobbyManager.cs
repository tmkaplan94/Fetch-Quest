using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    #region Private Serialized Fields

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players " +
             "and a new room will be created.")]
    [SerializeField] private byte maxPlayersPerRoom = 2;

    [Tooltip("Input field for getting room name from player.")]
    [SerializeField] private RoomNameInputField roomInputField;

    [Tooltip("The UI Panel to let the user enter name, connect and play")] 
    [SerializeField] private GameObject launcherPanel;

    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField] private GameObject connectingLabel;
    
    [Tooltip("The UI Label to inform the user that the connection is complete")]
    [SerializeField] private GameObject connectedLabel;

    #endregion


    #region Private Fields

    private string _gameVersion = "1";      // this client's version number
    private bool _needToCreateRoom = false;       // determines if new room needs to be created

    #endregion


    #region MonoBehavior Callbacks
    
    private void Awake()
    {
        // all clients in the same room will automatically sync level
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        // sets the proper display panel/label
        launcherPanel.SetActive(true);
        connectingLabel.SetActive(false);
        connectedLabel.SetActive(false);
    }

    #endregion


    #region MonoBahaviorPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");

        // get the most current room name set by the player
        string roomName = roomInputField.GetPlayerPrefRoomName();
        
        // create room before joining if necessary
        if (_needToCreateRoom)
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions() {MaxPlayers = maxPlayersPerRoom});
        }
        else
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);

        // sets the proper display panel/label
        launcherPanel.SetActive(true);
        connectingLabel.SetActive(false);
        connectedLabel.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
        
        // sets the proper display panel/label
        launcherPanel.SetActive(false);
        connectingLabel.SetActive(false);
        connectedLabel.SetActive(true);
    }
    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("OnJoinRoomFailed() was called by PUN.");
        
        // failed to join room, so create room
        string roomName = roomInputField.GetPlayerPrefRoomName();
        Debug.Log("Calling PhotonNetwork.CreateRoom()");
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() {MaxPlayers = maxPlayersPerRoom});
    }

    #endregion


    #region Public Methods
    
    // create and join room
    public void CreateAndJoinRoom()
    {
        _needToCreateRoom = true;
        Connect();
    }
    
    // join room
    public void JoinRoom()
    {
        _needToCreateRoom = false;
        Connect();
    }

    #endregion

    
    #region Private Methods

    // start the connection process
    private void Connect()
    {
        // sets the proper display panel/label
        launcherPanel.SetActive(false);
        connectingLabel.SetActive(true);
        connectedLabel.SetActive(false);

        // join a random room if connected to server, otherwise connect to server
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
    }

    #endregion
    
}
