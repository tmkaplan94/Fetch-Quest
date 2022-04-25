using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PettingState : IState
{
    private readonly AIController _controller;

    private float waitTime;

    public PettingState(AIController c)
    {
        _controller = c;
    }

    public void Tick()
    {
        if (waitTime <= Time.time)
        {
            _controller.GetNewTarget();
            //Debug.Log(_controller.dogNearby);
        }
    }

    public void OnEnter()
    {
        Debug.Log("petting"); 
        _controller.dogNearby = false;
        waitTime = Time.time + .1f;

    }

    public void OnExit()
    {
        Debug.Log("Exitting petting");
    }
}
