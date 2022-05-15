using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CleaningState : IState
{
    private readonly AIController _controller;
    private float waitTime;
    private Collider pee;
    public CleaningState(AIController c, Collider t)
    {
        _controller = c;
        pee = t;
    }

    public void Tick()
    {
        Debug.Log("Janitor Waiting");
        if (waitTime <= Time.time)
        {
            _controller.peeFound = false;
            _controller.CallDestroy(pee); //Does Not Work /shrug
            _controller.GetNewTarget();
        }
    }

    public void OnEnter()
    {
        Debug.Log("Janitor Enter Cleaning");
        waitTime = Time.time + _controller.AIStats.RestTime;
        //Play Animation
    }

    public void OnExit()
    {
    }
}
