using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : QuestItem
{
    QuestBus questBus;

    void Start()
    {
        questBus = LevelStatic.currentLevel.questBus;
    }
    
    public override void hitNPC(GameObject npc)
    {
        base.hitNPC(npc);

        QuestObject update = new QuestObject(10, "Who's a good doggo!");
        questBus.update(update);

        SpawnBones.Instance.SpawnNewBone();
        AudioManager.Instance.PlaySFX(AudioNames.ScoreUp, transform.position);

        Destroy(gameObject);
    }
}
