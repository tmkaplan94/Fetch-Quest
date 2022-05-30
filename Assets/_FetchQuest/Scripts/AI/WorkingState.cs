using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingState : IState
{
    private readonly AIController _controller;

    private float waitTime;

    public WorkingState(AIController c)
    {
        _controller = c;
    }

    public void Tick()
    {
        Debug.Log("Working Tick");
        if (_controller.fireAlarm)
        {
            Debug.Log("Working Tick fireAlarm");
            _controller.GetNewTarget();
        }
        if (waitTime <= Time.time)
        {
            Debug.Log("Working Tick waitTime");
            _controller.GetNewTarget();
        }
    }

    public void OnEnter()
    {
        Debug.Log("Working");
        waitTime = Time.time + _controller.AIStats.RestTime * 3;
    }

    public void OnExit()
    {
        Debug.Log("Working Done");
    }
}
