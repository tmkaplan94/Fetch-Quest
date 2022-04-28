using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicInScene : MonoBehaviour
{
    [SerializeField] private string music;
    void Start()
    {
        AudioManager.Instance.PlayMusic(music);
    }

}
