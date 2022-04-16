using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    [SerializeField] private AIStats _stats;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform dogTarget; //Dog Target

    private int currentWaypoint = 0;
    private StateMachine _stateMachine;
#nullable enable
    public Transform? Target { get; private set; }

    public AIStats AIStats => _stats;

    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        _stateMachine = new StateMachine();

        var walkingState = new WalkingState(this, navMeshAgent);
        var idleState = new IdleState(this);
        var pettingState = new PettingState(this); //Setting up PettingState

        At(idleState, walkingState, HasTarget());
        At(walkingState, idleState, ReachedDestination());
        At(pettingState, walkingState, HasTarget()); //For now only allow to transition from petting to walking
        Aat(pettingState, TargetNear()); //Adding petting state as an any


        _stateMachine.SetState(idleState);
    }
    void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
    void Aat(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition); //Shorthand for AddAnyTransition
    

    Func<bool> HasTarget() => () => Target != null;
    Func<bool> TargetNear() => () => Mathf.Infinity <= _stats.PettingDistance; //Is dog near?
    Func<bool> ReachedDestination() => () => Target != null && Vector3.Distance(transform.position, Target.position) < 1f;



    // Here is where the code is actually ran! Update function calls State Machine's Tick() every frame.
    //State Machine's Tick() checks if it needs to change states based on the transitions set above then calls tick() on current state.
    private void Update() => _stateMachine.Tick();

    public void SetTarget(Transform t)
    {
        Target = t;
        
        print("target set");
    }

    public void GetNewTarget()
    {
        print("getting Target " + currentWaypoint);

        if (currentWaypoint >= waypoints.Length -1)
            currentWaypoint = 0;
        else
            currentWaypoint++;
       
        
        SetTarget(waypoints[currentWaypoint]);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        _stateMachine.SetState(new PettingState(this));
    }
}
