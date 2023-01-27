using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is NULL!");

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private float _timer = 120f;
    [SerializeField]
    private bool _isTimerRunning = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isTimerRunning == true)
        {
            if (_timer >= 0)
            {
                _timer -= Time.deltaTime;
                UIManager.Instance.UpdateTimeRemaining(_timer, false);
            }
            else
            {
                Debug.Log("Timer has hit 0");
                _timer = 0;
                UIManager.Instance.UpdateTimeRemaining(_timer, true);
                _isTimerRunning = false;
                SpawnManager.Instance.StartSpawning();
            }
        }
    }
}
