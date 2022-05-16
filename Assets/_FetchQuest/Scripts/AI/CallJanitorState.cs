using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CallJanitorState : IState
{
    private readonly AIController _controller;
    private float waitTime;
    public CallJanitorState(AIController c, NavMeshAgent n)
    {
        _controller = c;
    }

    public void Tick()
    {
        //Play Wave animation while calling
        if (waitTime <= Time.time)
        {
            _controller.GetNewTarget();
        }
    }

    public void OnEnter()
    {
        //Look for Janitor
    }

    public void OnExit()
    {
    }
}
