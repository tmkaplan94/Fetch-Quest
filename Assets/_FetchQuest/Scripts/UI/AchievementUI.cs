using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Daichi M

// little intermediary to make editing the text easier
// also listens to the quest bus to self update

public class AchievementUIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _rarity;
    [SerializeField] private GameObject _uiGameObject;
    [SerializeField] private int _displayTime = 1;
    private QuestBus _questBus;
    
    public void Start()
    {
        _questBus = LevelStatic.currentLevel.questBus;
        _questBus.subscribe(displayFromQuestObject);

        hide();
    }

    public void displayFromQuestObject(QuestObject update)
    {
        if (update.display)
        {
            displayAchievement(update.message, update.rarity, _displayTime);
        }
        
    }
    
    public void displayAchievement(string description, string rarity, int time)
    {   
        show();

        AudioManager.Instance.PlaySFX(AudioNames.Achievement, transform.position);

        _description.SetText(description);
        _rarity.SetText(rarity);

        StartCoroutine(hideCoroutine(time));
    }

    private IEnumerator hideCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        hide();
    }

    private void show()
    {
        _uiGameObject.SetActive(true);
    }

    private void hide()
    {
        _uiGameObject.SetActive(false);
    }
}
