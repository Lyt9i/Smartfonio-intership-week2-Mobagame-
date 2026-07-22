using System;
using UnityEngine;
[Serializable]
public struct AttackPriority
{
    public string Enemy;
    [Tooltip("Первым выбираем с наибольшим"), Range(0,99)] public int Priority;
    public AttackPriority(Type type,int priority)
    {
        Enemy = type.FullName;
        Priority = priority;
    }
    public Type EnemyType()
    {
        return Type.GetType(Enemy);
    }
}