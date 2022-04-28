using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMainMenuTheme : MonoBehaviour
{
    
    void Start()
    {
        AudioManager.Instance.PlayMusic(AudioNames.MainTheme);
    }
}
