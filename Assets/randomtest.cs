using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomtest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("Strolling Along");
    }
    void Update()
    {
        if(Input.GetKeyDown("g"))
            AudioManager.Instance.PlaySFX("Pick_Up_SFX");
        if(Input.GetKeyDown("h"))
            AudioManager.Instance.PlaySFX("Score_Up_SFX");
    }
}
