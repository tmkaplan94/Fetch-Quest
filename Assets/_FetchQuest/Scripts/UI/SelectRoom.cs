using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectRoom : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;
    private InputField _roomInputField;

    private void Awake()
    {
        var array = FindObjectsOfType<InputField>();

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].name == "Room InputField")
            {
                _roomInputField = array[i];
                break;
            }
        }
    }

    public void Select()
    {
        _roomInputField.text = roomName.text;
    }
}