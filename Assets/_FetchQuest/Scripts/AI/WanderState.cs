using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState :  IState
{
    private readonly AIController _controller;
    public WanderState(AIController c)
    {
        _controller = c;
    }

    public void Tick()
    {

    }
    public void OnEnter()
    {
    }

    public void OnExit()
    { }
}
