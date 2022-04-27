using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testsound : MonoBehaviour
{
    public Transform _pos;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(AudioNames.IngameTheme);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("g")){
            AudioManager.Instance.PlaySFX(AudioNames.PickUp, _pos.position);
        }
        if(Input.GetKeyDown("h")){
            AudioManager.Instance.PlaySFX("General_Bark", _pos.position);
        }
    }
}
