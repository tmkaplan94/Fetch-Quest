/*
 * Author: Tyler Kaplan, Daichi Murokami
 * Contributors:
 * Summary: Manages the score for singleplayer.
 *
 * Description
 * - IncrementScore(int amount) can be called from anywhere
 * - amount parameter will reflect on the score board
 * - displays visual feedback for the player, also based on amount
 * - Daichi - for testing, hooks up to receive updates from the bus
 *
 * Updates
 * - PLEASE REMOVE UPDATE() FUNCTION, FOR TESTING PURPOSES ONLY !!
 */
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Private Serialized Fields

    [SerializeField] private TMP_Text currentScore;
    [SerializeField] private GameObject updateText;
    [SerializeField] private TMP_Text updateScore;
    [SerializeField] private int displaySecs;

    // bus testing
    [SerializeField] private QuestBus questBus; 
    
    #endregion


    #region Properties
    
    public int Score { get; private set; }
    
    #endregion


    #region MonoBehavior Callbacks

    // initialize score board
    private void Start()
    {
        // hookup, listens to bus
        questBus.subscribe(UpdateFromQuestObject);
        
        Score = 0;
        UpdateCurrentScoreText();
    }

    /****************************************************
     * REMOVE THIS !
     * - call IncrementScore(int amount) from wherever
     * NO
     ***************************************************/
    

    #endregion


    #region Public Methods

    // updates score and visual feedback based on amount
    public void IncrementScore(int amount)
    {
        // hijacking  this to test
        questBus.update(new QuestObject(amount, "test quest 2!"));

        // Score += amount;
        // DisplayUpdateText(amount.ToString());
        // UpdateCurrentScoreText();
    }
    
    #endregion


    #region Private Methods

    // processes quest objects and displays
    private void UpdateFromQuestObject(QuestObject quest)
    {
        Score += quest.pointsAwarded;
        DisplayUpdateText(quest.message);
        UpdateCurrentScoreText();
        
    }
    
    // updates displayed score
    private void UpdateCurrentScoreText()
    {
        currentScore.text = Score.ToString();
    }

    // shows visual feedback for a certain amount of displaySecs
    private void DisplayUpdateText(string amount)
    {
        updateScore.text = "+" + amount;
        StartCoroutine(BrieflyShowTextCoroutine());
    }

    // activates updateText for set amount of displaySecs
    private IEnumerator BrieflyShowTextCoroutine()
    {
        updateText.SetActive(true);
        yield return new WaitForSeconds(displaySecs);
        updateText.SetActive(false);
    }

    #endregion
}