using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serialized Fields
    
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players " +
             "and a new room will be created.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    
    #endregion

    
    #region Private Fields
    
    // this client's version number
    private string _gameVersion = "1";
    
    #endregion
    
    
    #region MonoBehavior Callbacks

    
    private void Awake()
    {
        // all clients in the same room will automatically sync level
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    #endregion

    
    #region MonoBahaviorPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        
        // attempt to join an existing room. if not, OnJoinRandomFailed() will be called
        PhotonNetwork.JoinRandomRoom();
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN.");
        
        // failed to join a random room, so create a new room
        Debug.Log("Calling PhotonNetwork.CreateRoom()");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
    }

    #endregion

    
    #region Public Methods
    
    // start the connection process
    public void Connect()
    {
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
