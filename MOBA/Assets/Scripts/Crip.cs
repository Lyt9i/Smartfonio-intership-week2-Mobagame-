using UnityEngine;

public class Crip : MovebleUnit,INeedTarget
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
}
