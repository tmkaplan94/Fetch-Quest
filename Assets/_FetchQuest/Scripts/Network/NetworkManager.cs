using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviorPunCallbacksSingleton<NetworkManager>
{
    #region Private Serialized Fields

    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private InputField playerName;
    [SerializeField] private InputField roomName;

    #endregion


    #region Private Fields

    private RoomOptions _roomOptions = new RoomOptions();

    #endregion


    // #region Properties
    //
    // public static GameSettings GameSettings => Instance.gameSettings;
    //
    // #endregion


    #region MonoBehavior Callbacks

    private void Awake()
    {
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
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarningFormat("Room creation failed because {0}", message);
    }

    #endregion
    

    #region Public Methods

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogWarning("Cannot attempt to create room, not connected to Photon servers");
            return;
        }
        
        string enteredNameValue = playerName.text;
        if (enteredNameValue == "")
        {
            playerName.text = gameSettings.DefaultPlayerName + GetRandomNumber();
        }
        
        string enteredRoomValue = roomName.text;
        if (enteredRoomValue == "")
        {
            roomName.text = gameSettings.DefaultRoomName + GetRandomNumber();
        }
        
        PhotonNetwork.CreateRoom(roomName.text, _roomOptions, TypedLobby.Default);
    }

    #endregion

    
    #region Private Methods

    private int GetRandomNumber()
    {
        return Random.Range(0, 10000);
    }

    #endregion

}