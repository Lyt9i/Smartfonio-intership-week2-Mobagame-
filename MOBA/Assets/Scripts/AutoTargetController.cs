using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(TeamTag))]
public class AutoTargetController : TargetController, INeedTarget
{
   [SerializeField] private float _viewDistance;
   [SerializeField] 
   private List<AttackPriority> _priorities = new List<AttackPriority>
        {
            new AttackPriority(typeof(Crip), 20),
            new AttackPriority(typeof(Base), 1),
        };
    // надо дописать
    public void SetPotentialTarget(List<Unit> potentialTargets)
    {
        //return;
    }
    // надо lдописать
    private Unit FindEnemyBase()
    {
        var bases = GamePlayManager.Instance.GetEnemiesBases(_teamTag);
        return bases[0];
    }
    private Unit GetMostTarget(List<Unit> potentialTargets)
    {
        var minDistance = float.MaxValue;
        var maxPriority = int.MinValue;
        Unit mostTarget = default;
        foreach (var target in potentialTargets)
        {
            var priority = GetPriority(target);
            if (priority < maxPriority)
            {
                continue;
            }
            maxPriority = priority;
            var distance = Vector3.Distance(target.Position,Position);
            if (distance >= minDistance)
            {
                continue;
            }
            minDistance = distance;
            mostTarget = target;
        }
        return mostTarget == null ? FindEnemyBase() : mostTarget;
    }
    private int GetPriority(Unit type)
    {
        foreach (var data in _priorities)
        {
            if ((data.EnemyType() == type.GetType()) || type.GetType().IsSubclassOf(data.EnemyType()))
            {
                return data.Priority;
            }
        }
        return int.MinValue;
    }
    public Vector3 Position => transform.position;
    public float GetViewDistance() => _viewDistance;
}
