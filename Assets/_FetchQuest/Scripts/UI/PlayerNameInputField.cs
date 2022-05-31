using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerNameInputField : MonoBehaviour
{
    #region Constants

    // store PlayerPref key to avoid typos
    private const string PlayerNamePrefKey = "PlayerName";

    #endregion


    #region MonoBehavior Callbacks
    
    private void Start ()
    {
        //GetPlayerPrefPlayerName();
    }
    
    #endregion


    #region Public Methods
    
    // sets the name of the player and saves it in the PlayerPrefs for future sessions
    public void SetPlayerPrefPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;
        //PlayerPrefs.SetString(PlayerNamePrefKey, value);
    }
    
    #endregion


    #region Private Methods

    // get the player's stored name based on PlayerPrefs
    private void GetPlayerPrefPlayerName()
    {
        string defaultName = "name";
        
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