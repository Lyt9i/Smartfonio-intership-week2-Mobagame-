using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance { get; private set; }
    [SerializeField] private List<Unit> _allUnits = new List<Unit>();

    [SerializeField] private int _amountTeams = 2;
    [SerializeField] private float _delay = 0.5f;
    private float _currentDelay;

    public event Action<Unit> onAdded;
    public event Action<Unit> onRemoved;
    private float _battleTime;

    private void Awake()
    {
        Instance = this;
        _battleTime = 0;
        _currentDelay = _delay;
    }

    private void Update()
    {
        _battleTime += Time.deltaTime;
        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
            return;
        }
        _currentDelay = _delay;
        CheckInterceptions();
    }

    private List<Unit> GetAllAllies(int teamId)
    {
        return Find<Unit>(u => u.GetTeamTag().GetTeamId() == teamId);
    }

    private List<Unit> GetAllEnemies(int teamId)
    {
        return Find<Unit>(u => u.GetTeamTag().GetTeamId() != teamId);
    }
    private void CheckInterceptions()
    {
        var potentialTargets = new List<Unit>(10);
        for (var i = 1; i <= _amountTeams; i++)
        {
            var units = GetAllAllies(i);
            var enemies = GetAllEnemies(i);
            foreach (var unit in units)
            {
                if (unit is INeedTarget attacker)
                {
                    potentialTargets.Clear();
                    var viewDistance = attacker.GetViewDistance();
                    foreach (var enemy in enemies)
                    {
                        if (Vector3.Distance(enemy.Position, unit.Position) <= viewDistance)
                        {
                            potentialTargets.Add(enemy);
                        }
                    }
                    attacker.SetPotentialTarget(new List<Unit>(potentialTargets));
                }
            }
        }
    }
    public List<T> Find<T>(Func<T, bool> predicate) where T : Unit
    {
        var result = new List<T>();
        foreach (var unit in _allUnits)
        {
            if (unit is T t)
            {
                if (predicate(t))
                {
                    result.Add(t);
                }
            }
        }
        return result;
    }
    public void Register(Unit unit)
    {
        _allUnits.Add(unit);
        onAdded?.Invoke(unit);
        FindMyTarget(unit);
        
    }
    private void FindMyTarget(Unit unit)
    {
        if (!(unit is INeedTarget needTarget))
        {
            return;
        }
        var target = Find<Base>(u=>u.GetTeamTag().GetTeamId() != unit.GetTeamTag().GetTeamId())[0];
        needTarget.SetTarget(target);
    }
    public void Unregister(Unit unit)
    {
        _allUnits.Remove(unit);
        onRemoved?.Invoke(unit);
    }
    public List<Base> GetEnemiesBases(TeamTag teamTag)
    {
        return Find<Base>(u => u.GetTeamTag().GetTeamId() != teamTag.GetTeamId());
    }

    
}
