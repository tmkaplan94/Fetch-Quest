using UnityEngine;

[CreateAssetMenu(fileName = "NetworkManager", menuName = "ScriptableObject/NetworkManager")]
public class NetworkManager : ScriptableObjectSingleton<NetworkManager>
{
    #region Private Serialized Fields

    [SerializeField] private GameSettings gameSettings;

    #endregion


    #region Properties

    public static GameSettings GameSettings => Instance.gameSettings;

    #endregion
}