using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    #region Constants

    // store the PlayerPref Key to avoid typos
    private const string PlayerNamePrefKey = "PlayerName";

    #endregion


    #region MonoBehavior Callbacks
    
    private void Start ()
    {
        GetPlayerPrefName();
    }
    
    #endregion


    #region Public Methods
    
    // sets the name of the player and saves it in the PlayerPrefs for future sessions
    public void SetPlayerPrefName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(PlayerNamePrefKey, value);
    }
    
    #endregion


    #region Private Methods

    // get the player's stored name based on PlayerPrefs
    private void GetPlayerPrefName()
    {
        string defaultName = "";
        InputField inputField = GetComponent<InputField>();
        if (inputField != null)
        {
            if (PlayerPrefs.HasKey(PlayerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
                inputField.text = defaultName;
            }
        }
        
        PhotonNetwork.NickName =  defaultName;
    }

    #endregion
}