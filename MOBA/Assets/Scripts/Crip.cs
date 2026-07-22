using UnityEngine;

public class Crip : MovebleUnit
{
    private Unit _target;
    private void Update()
    {
        if (_target != null)
        {
            SetDestination(_target.Position);
        }
        
    }
    private void SetTarget(Unit unit)
    {
        _target = unit;
    }
}
