using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriers : MonoBehaviour, IDamageable
{

    [SerializeField]
    private int _health;
    
    public void Damage()
    {
        throw new System.NotImplementedException();
    }
}
