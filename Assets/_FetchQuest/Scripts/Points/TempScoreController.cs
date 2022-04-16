using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/* ==========
    Daichi Murokami:

    for testing my score classes, move this to the controller thx.

    ========== */
public class TempScoreController : MonoBehaviour
{

    PlayerScore _playerScore;

    Text text;


    // Start is called before the first frame update
    void Start()
    {
        _playerScore = gameObject.GetComponent(typeof(PlayerScore)) as PlayerScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            _playerScore.AddPet();
        }
    }
}
