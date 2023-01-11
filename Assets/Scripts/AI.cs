using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private enum AIState
    {
        Run,
        Hide,
        Death
    }

    private AIState _currentState;

    private NavMeshAgent _agent;
    [SerializeField]
    private Transform _endPoint;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
            Debug.LogError("NavMeshAgent is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_endPoint.position);
    }

    //Run to end point,
    //randomly Hide at a hide point. But forward.
    //Hide behind pillar for a random amount of time. 
    //Death, Trigger death animation
    //Add 50 Points.
}
