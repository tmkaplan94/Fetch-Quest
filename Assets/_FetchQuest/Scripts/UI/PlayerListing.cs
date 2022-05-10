using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListing : MonoBehaviour
{
    #region Private Serialized Fields

    [SerializeField] private TMP_Text info;

    #endregion


    #region Public Methods

    public void SetInformation(Player player)
    {
        info.text = player.NickName;
    }

    #endregion
}