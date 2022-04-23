using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ==========
    Daichi Murokami:

    for testing my score classes, move this to the controller thx.

    ========== */
public class TempScoreController : MonoBehaviour
{

    private PlayerScore _playerScore;

    public Text text;
    public ScoreManager scoreManager;

    public void UpdateText(GameObject player, int points)
    {
        text.text = "Pets: " + points;
    }


    // Start is called before the first frame update
    void Start()
    {
        _playerScore = gameObject.GetComponent(typeof(PlayerScore)) as PlayerScore;
        scoreManager.pointsDelegate += UpdateText;
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
