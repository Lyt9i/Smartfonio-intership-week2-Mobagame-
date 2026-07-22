using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance { get; private set; }
    [SerializeField] private List<Unit> _allUnits = new List<Unit>();
    
    public event Action<Unit> onAdded;
    public event Action<Unit> onRemoved;
    private void Awake()
    {
        Instance = this;
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
    }
    public void Unregister(Unit unit)
    {
        _allUnits.Remove(unit);
        onRemoved?.Invoke(unit);
    }
    
}
