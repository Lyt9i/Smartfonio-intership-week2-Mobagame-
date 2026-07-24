using UnityEngine;
using System.Collections.Generic;

public class Crip : MovebleUnit, INeedTarget
{
    
    private Unit _target;
    private TargetController _targetController;
    private WeaponHandler _weaponHandler;
    private void Start()
    {
       _targetController = GetComponent<TargetController>();
        _weaponHandler = GetComponent<WeaponHandler>();
        Initialize();
        
    }
    private void Update()
    {
        if (_target == null) return;

        var distance = Vector3.Distance(_target.Position, Position);
        var attackRange = _weaponHandler.GetWeapon().GetAttackRange();

        if (distance > attackRange)
        {
            SetDestination(_target.Position);
        }
        else
        {
            SetDestination(Position); // стоп, атака берёт на себя AutoAttacker
        }
    }
    public void SetTarget(Unit unit)
    {
        _target = unit;
        _targetController?.SetTarget(unit);
        
    }
    public void SetPotentialTarget(List<Unit> potentialTargets)
    {
        if (potentialTargets == null || potentialTargets.Count == 0)
        {
            if (_target == null) 
            {
                var enemyBase = GamePlayManager.Instance.GetEnemiesBases(GetTeamTag())[0];
                SetTarget(enemyBase);
            }
            return; 
        }

        Unit nearest = null;
        var minDistance = float.MaxValue;
        foreach (var enemy in potentialTargets)
        {
            var distance = Vector3.Distance(enemy.Position, Position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }

        if (nearest != null)
        {
            SetTarget(nearest);
        }

    }
    public float GetViewDistance()
    {
            return 10f; // TODO: реализовать логику получения дальности зрения
    }
}
