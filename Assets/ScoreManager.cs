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
    
    #endregion


    #region Properties
    
    public int Score { get; private set; }
    
    #endregion


    #region MonoBehavior Callbacks

    private void Start()
    {
        Score = 0;
        UpdateCurrentScoreText();
    }

    /****************************************************
     * REMOVE THIS !
     * - call IncrementScore(int amount) from wherever
     ***************************************************/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncrementScore(1);
        }
    }

    #endregion


    #region Public Methods

    public void IncrementScore(int amount)
    {
        Score += amount;
        DisplayUpdateText(amount);
        UpdateCurrentScoreText();
    }
    
    #endregion


    #region Private Methods

    private void UpdateCurrentScoreText()
    {
        currentScore.text = Score.ToString();
    }

    private void DisplayUpdateText(int amount)
    {
        updateScore.text = "+" + amount;
        StartCoroutine(BrieflyShowTextCoroutine());
    }

    private IEnumerator BrieflyShowTextCoroutine()
    {
        updateText.SetActive(true);
        yield return new WaitForSeconds(displaySecs);
        updateText.SetActive(false);
    }

    #endregion
}