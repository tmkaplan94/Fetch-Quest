using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private readonly AIController _controller;

    private float waitTime;
    
    public IdleState(AIController c)
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
        Debug.Log("idling");
        waitTime = Time.time + _controller.AIStats.RestTime;
    }

    public void OnExit()
    {
        _controller.idelCount += 1;
    }
}
