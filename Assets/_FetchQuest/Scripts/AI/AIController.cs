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

    private Transform currentWaypoint;
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

        At(idleState, walkingState, HasTarget());
        At(walkingState, idleState, ReachedDestination());


        _stateMachine.SetState(idleState);
    }
    void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

    Func<bool> HasTarget() => () => Target != null;
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
        print("getting Target");

        int waypointIdx = Random.Range(0, waypoints.Length - 1);
        while(Target != null && waypoints[waypointIdx] == Target)
        {
            waypointIdx = Random.Range(0, waypoints.Length - 1);
        }
        SetTarget(waypoints[waypointIdx]);
        
    }
}
