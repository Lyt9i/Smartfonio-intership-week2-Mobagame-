using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TeamTag))]
[SelectionBase]
public abstract class Unit : MonoBehaviour
{
    // Начисление очков за убийство юнита
    [SerializeField] protected int _killReward = 0;
    public int GetKillReward() => _killReward;
    protected Health _health;
    protected TeamTag _teamTag;
    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
        _teamTag = GetComponent<TeamTag>();
    }
    public virtual void Initialize()
    {
        _health.Initialize();
        _health.onDie += Die;
        GamePlayManager.Instance.Register(this);
    }
    private void Die()
    {
        Destroy(gameObject, 1f);
        GamePlayManager.Instance.Unregister(this);
    }
    public TeamTag GetTeamTag()
    {
        return _teamTag;
    }
    public Health GetHealth()
    {
        return _health;
    }
    public Vector3 Position => transform.position;
    
}

