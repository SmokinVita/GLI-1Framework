using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    //Singleton system.
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SpawnManager is NULL");

            return _instance;
        }
    }

    //Spawn AI at Starting point
    //Learn about pooling.

    [SerializeField]
    private GameObject _aiPrefab;
    [SerializeField]
    private Transform _spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            GameObject enemyAI = Instantiate(_aiPrefab, _spawnPoint.position, Quaternion.identity);
            enemyAI.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(5f);
        }
    }
}
