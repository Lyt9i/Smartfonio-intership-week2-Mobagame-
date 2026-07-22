using System;
using UnityEngine;

public abstract class TargetController : MonoBehaviour
{
    [SerializeField] private Unit _curentTarget;
    public event Action<Unit> onTargetChanged;
    protected TeamTag _teamTag;
    protected virtual void Awake()
    {
        _teamTag = GetComponent<TeamTag>();
    }
    public virtual void SetTarget(Unit target)
    {
        if (_curentTarget == target) return;
        _curentTarget = target;
        onTargetChanged?.Invoke(target);
    }
}
