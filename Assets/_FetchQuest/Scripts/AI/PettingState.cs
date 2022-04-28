using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PettingState : IState
{
    private readonly AIController _controller;

    //This is the length of the petting animation. (so changing this will change how long the animation lasts as well as 
    //the wait time.) The animation will play once but in super slow motion if you make this number big. Or
    //it will play once but super fast if you make this number small.
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
