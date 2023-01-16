using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour, IDamageable
{
    private enum AIState
    {
        Run,
        Hide,
        Death
    }

    [SerializeField]
    private AIState _currentState;

    private Collider _collider;
    private Animator _anim;
    private NavMeshAgent _agent;
    [SerializeField]
    private Transform _endPoint;

    [SerializeField]
    private List<GameObject> _barriers = new List<GameObject>();
    private GameObject _barriersToFind;

    private bool _isHiding = false;
    private bool _isDead = false;

    private float _timeTillHiding;
    private float _currentTime;

    void OnEnable()
    {
        _timeTillHiding = Random.Range(5, 10);
        _currentTime = Time.time + _timeTillHiding;
    }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
            Debug.LogError("NavMeshAgent is NULL");

        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
            Debug.LogError($"Animator is NULL for {this.gameObject}");

        _collider = GetComponent<Collider>();
        if (_collider == null)
            Debug.LogError("The Collider is NULL");

        foreach (var walls in GameObject.FindGameObjectsWithTag("Barrier"))
        {
            _barriers.Add(walls);
        }
    }



    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case AIState.Run:
                CalculateMovement();
                break;
            case AIState.Hide:
                if (_isHiding == false)
                {
                    //call coroutine;
                    StartCoroutine(HidingRoutine());
                    _isHiding = true;
                    _currentTime = Time.time + _timeTillHiding;
                }
                break;
            case AIState.Death:
                //add 50 points to GameManager/UIManager
                if (_isDead == false)
                {
                    _isDead = true;
                    StartCoroutine(DeathRoutine());
                }
                break;
        }
    }

    void CalculateMovement()
    {
        _agent.SetDestination(_endPoint.position);
        _anim.SetFloat("Speed", _agent.speed);

        if (Time.time > _currentTime)
        {
            FindClosestBarrier();
        }
    }

    void FindClosestBarrier()
    {
        foreach (var barrier in _barriers)
        {
            var distance = Vector3.Distance(barrier.transform.position, transform.position);
            if (distance <= 3f)
            {
                _agent.SetDestination(barrier.transform.position);
                if(distance <= 2.5f)
                {
                    _agent.isStopped = true;
                    _currentState = AIState.Hide;
                    _currentTime = Time.time + _timeTillHiding;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPoint"))
        {
            this.gameObject.SetActive(false);
        }
    }

    //Death, Trigger death animation
    //Add 50 Points.
    public void Damage()
    {
        _currentState = AIState.Death;
    }

    IEnumerator HidingRoutine()
    {
        _anim.SetBool("Hiding", true);
        float hideTime = Random.Range(1, 6);
        _agent.isStopped = true;
        yield return new WaitForSeconds(hideTime);
        _agent.isStopped = false;
        _currentState = AIState.Run;
        _isHiding = false;
        _anim.SetBool("Hiding", false);
    }

    IEnumerator DeathRoutine()
    {
        _agent.isStopped = true;
        _collider.enabled = false;
        _anim.SetBool("Death", true);
        yield return new WaitForSeconds(4f);
        this.gameObject.SetActive(false);
        _collider.enabled = true;
        _currentState = AIState.Run;
        _isDead = false;
    }
}
