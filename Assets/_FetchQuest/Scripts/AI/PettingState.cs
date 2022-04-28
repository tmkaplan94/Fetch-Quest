using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PettingState : IState
{
    private readonly AIController _controller;

    private float waitTime = 1.987f;
    private float timeToWaitTil;

    public PettingState(AIController c)
    {
        _controller = c;
    }

    public void Tick()
    {
        if (timeToWaitTil <= Time.time)
        {
            Debug.Log("leave");
            _controller.GetNewTarget();
            //Debug.Log(_controller.dogNearby);
        }
    }

    public void OnEnter()
    {
        Debug.Log("petting"); 
        _controller.dogNearby = false;
        float animSPeed = 1.867f / waitTime;
        _controller.AnimationStart(animSPeed);
        timeToWaitTil = Time.time + waitTime;
    }

    public void OnExit()
    {
        Debug.Log("Exitting petting");
        _controller.AnimationStop();
    }
}
