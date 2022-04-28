using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ==========
    Daichi Murokami:

    Player component for scoring, communicates with manager
    Allows gameobject to self manage/modify score;

    ========== */

public class PlayerScore : MonoBehaviour
{
    
    // injected by manager
    // assume player has been added to score/player manager
    // TODO is there any way to have the manager anonymously monitor this?
    [HideInInspector]
    public ScoreManager_old _scoreManager;


    // Call this
    public void AddPet(int pets = 1)
    {
        int score = _scoreManager.GetPlayerScore(gameObject);
        _scoreManager.SetPlayerScore(gameObject, score + pets);
    }


    // ===== CALLBACK FUNCS =====

    
    void Start()
    {
         
    }

    void Update()
    {
        
    }
}
