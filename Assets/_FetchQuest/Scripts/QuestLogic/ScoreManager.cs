/*
 * Author: Tyler Kaplan, Daichi Murokami
 * Contributors:
 * Summary: Manages the score for singleplayer.
 *
 * Description

 * - I HAVE HIJACKED THIS SCRIPT FOR TESTING THE EVENT BUS
 * - currently it both subscribes to and updates the bus

 * - IncrementScore(int amount) can be called from anywhere
 * - amount parameter will reflect on the score board
 * - displays visual feedback for the player, also based on amount
 *
 * Updates
 * - PLEASE REMOVE UPDATE() FUNCTION, FOR TESTING PURPOSES ONLY !!
 */
using System.Collections;
using TMPro;
using UnityEngine;
using Photon.Pun;
public class ScoreManager : MonoBehaviour
{
    #region Private Serialized Fields

    [SerializeField] private TMP_Text currentScore;
    [SerializeField] private GameObject updateText;
    [SerializeField] private TMP_Text updateScore;
    [SerializeField] private int displaySecs;

    // bus testing
    private QuestBus questBus;
    
    #endregion


    #region Properties
    
    public int Score { get; private set; }
    private bool isNetworked;

    #endregion


    #region MonoBehavior Callbacks
    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
        {
            isNetworked = false;
        }
        else
            isNetworked = true;
    }
    // initialize score board
    private void Start()
    {
        // hookup, listens to bus
        // has to wait until after parent starts
        if (LevelStatic.currentLevel != null)
        {
            questBus = LevelStatic.currentLevel.questBus; 
            questBus.subscribe(UpdateFromQuestObject);
            Debug.Log("HOOKED UP TO QUEST BUS");
        }
        else
        {
            Debug.Log("FAILED TO FIND LEVELDATA: " + LevelStatic.currentLevel);
        }
        
        
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
        if (isNetworked)
        {
            PhotonView v = GetComponent<PhotonView>();
            v.RPC("IncrementScoreRPC", RpcTarget.All, amount);
        }
        else
            IncrementScoreRPC(amount);
    }
    [PunRPC]
    public void IncrementScoreRPC(int amount)
    {
        // hijacking  this to test
        if (questBus != null)
            questBus.update(new QuestObject(amount, "dogs are good!", 
                            LevelData.publicEvents.NOEVENT, "helloQuest"));
        else
        {
            print("QUEST BUS IS NULL");
            Score += amount;
            DisplayUpdateText(amount.ToString());
            UpdateCurrentScoreText();
        }
    }
    
    #endregion


    #region Private Methods

    // processes quest objects and displays
    private void UpdateFromQuestObject(QuestObject quest)
    {
        Score += quest.pointsAwarded;
        // string display = quest.message + " enum: " + quest.eventEnum
        //         + " name: " + quest.questName;
        string display = quest.message;
        DisplayUpdateText(display);
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