using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriers : MonoBehaviour, IDamageable
{

    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private GameObject _forceField;
    private Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
        if (_collider == null)
            Debug.LogError("Collider is NULL on " + this.gameObject);
    }

    public void Damage()
    {
        _health--;
        Debug.Log("Barrier Got Hit!");
        if(_health <= 0)
        {
            _forceField.SetActive(false);
            _collider.enabled = false;
            StartCoroutine(RechargeRoutine());
        }
    }

    IEnumerator RechargeRoutine()
    {
        yield return new WaitForSeconds(5f);
        _health = 3;
        _forceField.SetActive(true);
        _collider.enabled = true;
    }
}