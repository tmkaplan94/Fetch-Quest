using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingState : IState
{
    private readonly AIController _controller;

    private float waitTime;

    public TalkingState(AIController c)
    {
        _controller = c;
    }

    public void Tick()
    {
        if (waitTime <= Time.time)
        {
            _controller.GetNewTarget();
        }
    }

    public void OnEnter()
    {
        Debug.Log("Talking");
        _controller.personNearby = false;
        waitTime = Time.time + _controller.AIStats.TalkTime * 2;
    }

    public void OnExit()
    {
    }
}
