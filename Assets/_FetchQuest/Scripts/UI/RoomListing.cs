using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomListing : MonoBehaviour
{
    #region Private Serialized Fields

    [SerializeField] private TMP_Text info;

    #endregion


    #region Public Methods

    public void SetInformation(RoomInfo room)
    {
        info.text = room.Name;
    }

    #endregion

}