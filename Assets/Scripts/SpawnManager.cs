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
            GameObject enemyAI = Pool_Manager.Instance.RequestAI();
            enemyAI.transform.position = _spawnPoint.position;
            yield return new WaitForSeconds(5f);
        }
    }
}
