using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    #region Constants

    // store PlayerPref key to avoid typos
    private const string PlayerNamePrefKey = "PlayerName";

    #endregion


    #region MonoBehavior Callbacks
    
    private void Start ()
    {
        UpdatePlayerPrefPlayerName();
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
        PlayerPrefs.SetString(PlayerNamePrefKey, value);
    }
    
    // gets the player's preferred room name based on PlayerPref
    public string GetPlayerPrefPlayerName()
    {
        return GetComponent<InputField>().text;
    }
    
    #endregion

    
    #region Private Methods
    
    // get the player's stored name based on PlayerPrefs
    private void UpdatePlayerPrefPlayerName()
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