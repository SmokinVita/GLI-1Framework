using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Manager : MonoBehaviour
{

    private static Pool_Manager _instance;
    public static Pool_Manager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Pool Manager is NULL");

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private int _enemyAISize = 10;
    [SerializeField]
    private List<GameObject> _aiPool = new List<GameObject>();
    [SerializeField]
    private GameObject _aiContainer;
    [SerializeField]
    private GameObject _enemyAIPrefab;

    void Start()
    {
        GenerateAI(_enemyAISize);
    }

    private List<GameObject> GenerateAI(int amountOfEnemyAI)
    {
        for (int i = 0; i < amountOfEnemyAI; i++)
        {
            GameObject EnemyAI = Instantiate(_enemyAIPrefab);
            EnemyAI.transform.parent = _aiContainer.transform;
            EnemyAI.SetActive(false);

            _aiPool.Add(EnemyAI);
        }

        return _aiPool;
    }

    public GameObject RequestAI()
    {
        foreach (var enemyAI in _aiPool)
        {
            if (enemyAI.activeInHierarchy == false)
            {
                enemyAI.SetActive(true);
                return enemyAI;
            }
        }

        GameObject newAI = Instantiate(_enemyAIPrefab);
        newAI.transform.parent = _aiContainer.transform;

        return newAI;
    }
}
