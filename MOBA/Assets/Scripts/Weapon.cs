using System;
using UnityEngine;
[Serializable]
public class Weapon
{
    [SerializeField] private float _attackRange;
    [SerializeField] private int _dammgeValue;
    [SerializeField] private float _attackInterval;

    public float GetAttackRange()
    {
        return _attackRange;
    }
    public int GetDamageValue()
    {
        return _dammgeValue;
    }
    public float GetAttackInterval()
    {
        return _attackInterval;
    }
}