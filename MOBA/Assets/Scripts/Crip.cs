using UnityEngine;
using System.Collections.Generic;

public class Crip : MovebleUnit, INeedTarget
{
    
    private Unit _target;
    private void Start()
    {
        Initialize();
    }
    private void Update()
    {
        if (_target != null)
        {
            SetDestination(_target.Position);
        }
        
    }
    public void SetTarget(Unit unit)
    {
        _target = unit;
    }
    public void SetPotentialTarget(List<Unit> potentialTargets)
    {
        return; // TODO: логика выбора цели для крипа
    }
    public float GetViewDistance()
    {
            return 10f; // TODO: реализовать логику получения дальности зрения
    }
}
