using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObject/GameSettings")]
public class GameSettings : ScriptableObject
{
    #region Private Serialized Fields

    [SerializeField] private string gameVersion = "1";
    [SerializeField] private byte maxPlayers = 2;
    [SerializeField] private string defaultPlayerName = "Doggo";
    [SerializeField] private string defaultRoomName = "Room";

    #endregion


    #region Properties

    public string GameVersion => gameVersion;
    public byte MaxPlayers => maxPlayers;
    public string DefaultPlayerName => defaultPlayerName;
    public string DefaultRoomName => defaultRoomName;

    #endregion
}