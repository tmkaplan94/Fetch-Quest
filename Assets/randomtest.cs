using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomtest : MonoBehaviour
{
    [SerializeField] private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(AudioNames.IngameTheme);
    }
    void Update()
    {
        if(Input.GetKeyDown("g"))
            AudioManager.Instance.PlaySFX(AudioNames.ScoreUp, gameObject.transform.position);
        if(Input.GetKeyDown("h"))
            AudioManager.Instance.PlaySFX(AudioNames.PickUp, _player.position);

    }
}
