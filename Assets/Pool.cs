using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pool : MonoBehaviour
{
    private int ballCount = 0;
    private bool loss = false;
    QuestBus eventSys;
    private bool isNetworked;
    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() != null)
            isNetworked = true;
        else
            isNetworked = false;
    }
    void Start()
    {
        eventSys = LevelStatic.currentLevel.questBus;
    }
    private void OnTriggerEnter(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerEnter(other, gameObject, OnTriggerExit);

        if(other.CompareTag("Billiard"))
        {
            if (other.gameObject.name == "WhiteBall")
                return;
            if (other.gameObject.name == "8Ball")
            {
                if (ballCount == 9 && !loss)
                    Win();
                else
                    Lose();
            }
            else
            {
                if(!loss)
                    eventSys.update(new QuestObject(5, "Nice Shot!", LevelData.publicEvents.POOL));
                else
                    eventSys.update(new QuestObject(0, "Start over to Try Again!", LevelData.publicEvents.POOL));
            }
            ballCount++;
            print(ballCount);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ReliableOnTriggerExit.NotifyTriggerExit(other, gameObject);
        if (other.CompareTag("Billiard"))
        {
            
            ballCount--;
            if (ballCount <= 0)
                loss = false;
            print(ballCount);
        }
    }
    private void Win()
    {
        if (isNetworked)
        {
            PhotonView v = GetComponent<PhotonView>();
            v.RPC("WinRPC", RpcTarget.All);
        }
        else
            WinRPC();
    }
    [PunRPC]
    private void WinRPC()
    {
        eventSys.update(new QuestObject(200, "You Won a game of Pool!", LevelData.publicEvents.POOL));
        AudioManager.Instance.PlaySFX(AudioNames.ScoreUp, transform.position);
    }
    private void Lose()
    {
        if (isNetworked)
        {
            PhotonView v = GetComponent<PhotonView>();
            v.RPC("LoseRPC", RpcTarget.All);
        }
        else
            LoseRPC();
    }
    [PunRPC]
    private void LoseRPC()
    {
        eventSys.update(new QuestObject(0, "8Ball goes last! Start over!", LevelData.publicEvents.POOL));
        loss = true;
    }
}
