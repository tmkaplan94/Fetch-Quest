using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObject/GameSettings")]
public class GameSettings : ScriptableObject
{
    #region Private Serialized Fields

    [SerializeField] private string gameVersion = "1";
    [SerializeField] private string nickName = "Doggo";

    #endregion


    #region Properties

    public string GameVersion => gameVersion;
    public string NickName => nickName;

    #endregion
}