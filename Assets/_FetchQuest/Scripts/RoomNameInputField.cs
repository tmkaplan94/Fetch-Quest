using UnityEngine;
using UnityEngine.UI;

public class RoomNameInputField : MonoBehaviour
{
    #region Constants

    // store PlayerPref key to avoid typos
    private const string RoomNamePrefKey = "RoomName";

    #endregion


    #region MonoBehavior Callbacks

    private void Start()
    {
        GetPlayerPrefRoomName();
    }

    #endregion


    #region Public Methods

    // sets the room name and saves it in the PlayerPrefs for future sessions
    public void SetPlayerPrefRoomName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Room Name is null or empty");
            return;
        }
        PlayerPrefs.SetString(RoomNamePrefKey, value);
    }
    
    #endregion


    #region Private Methods
    
    // gets the player's preferred room name based on PlayerPref
    private void GetPlayerPrefRoomName()
    {
        string defaultName = "";
        InputField inputField = GetComponent<InputField>();
        if (inputField != null)
        {
            if (PlayerPrefs.HasKey(RoomNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(RoomNamePrefKey);
                inputField.text = defaultName;
            }
        }
    }
    
    #endregion
}