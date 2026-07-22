using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private readonly Dictionary<int, int> _points = new Dictionary<int, int> { { 1, 0 }, { 2, 0 } };
    public event Action<int, int> onPointsChanged; 

    private void Awake() => Instance = this;

    public int GetPoints(int teamId) => _points[teamId];

    public void AddPoints(int teamId, int amount)
    {
        _points[teamId] += amount;
        onPointsChanged?.Invoke(teamId, _points[teamId]);
        Debug.Log($"Кол-во очков у команды {teamId}: {_points[teamId]}");
    }

    public bool TrySpend(int teamId, int amount)
    {
        if (_points[teamId] < amount) return false;
        _points[teamId] -= amount;
        onPointsChanged?.Invoke(teamId, _points[teamId]);
        Debug.Log($"Кол-во очков у команды {teamId}: {_points[teamId]}");
        return true;
        
    }

    public static int GetOtherTeam(int teamId) => teamId == 1 ? 2 : 1;
}