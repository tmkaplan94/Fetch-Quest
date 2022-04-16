using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoreManager : MonoBehaviour
{
    /* ==========
    Daichi Murokami:

    Stores and manages scores - sorry, kind of thrown together

    ========== */


    // ===== FIELDS =====


    delegate void OnPointsUpdated(GameObject player, int points);
    OnPointsUpdated pointsDelegate;

    // for initializing players
    // TODO - how are we going to do this? 
    // set in editor
    [SerializeField]
    private GameObject[] _players;
    
    // where the scores be
    private Dictionary<GameObject, int> _scores = new Dictionary<GameObject, int>();

    // cumulative scores
    private int _totalScore = 0;


    // ===== ACCESSORS =====


    public int GetTotalScore()
    {
        return _totalScore;
    }


    public int GetPlayerScore(GameObject player)
    {
        int score;
        Assert.IsTrue(_scores.TryGetValue(player, out score));
        return score;
    }


    public void SetPlayerScore(GameObject player, int score)
    {
        Assert.IsTrue(_scores.ContainsKey(player));
        AddScore(player, score);
    }


    // ===== HELPERS =====


    private void AddScore(GameObject player, int score)
    {
        // debug
        print("score manager, score updated: "+ player+ ", "+ score);
        _scores[player] = score;

        // plug into ui or something here
        pointsDelegate(player, score);
    }


    // ===== CALLBACK FUNCS =====


    void Start()
    {
        // populate dictionary from editor array, temp
        foreach (GameObject player in _players)
        {
            _scores.Add(player, 0);
        }

        // inject self reference into all monitored players
        foreach (GameObject player in _scores.Keys)
        {
            PlayerScore component = player.GetComponent(typeof(PlayerScore)) as PlayerScore;
            component._scoreManager = this;
        }
    }


    void Update()
    {
        
    }
}
