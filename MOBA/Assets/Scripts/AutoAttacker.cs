using UnityEngine;
[RequireComponent(typeof(WeaponHandler))]
[RequireComponent(typeof(TargetController))]
public class AutoAttacker : MonoBehaviour
{
    [SerializeField] private Unit _currentTarget;
    private WeaponHandler _weaponHandler;
    private TargetController _targetController;
    private float _currentDelay;

    protected void Awake()
    {
        _weaponHandler = GetComponent<WeaponHandler>();
        _targetController = GetComponent<TargetController>();
        _targetController.onTargetChanged += Change;
    }
    protected virtual void Update()
    {
        if (_currentTarget == null) return;
        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
            return;
        }
        if (Vector3.Distance(_currentTarget.Position, Position) > Weapon.GetAttackRange())
        {
            return;
        }
        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
            return;
        }
        _currentDelay = Weapon.GetAttackInterval();
        _currentTarget.GetHealth().Damage(Weapon.GetDamageValue());

    }
    public Vector3 Position => transform.position;
    public Weapon Weapon => _weaponHandler.GetWeapon();
    private void Change(Unit obj)
    {
        _currentTarget = obj;
    }
    private float GetAttackRange()
    {
        return Weapon.GetAttackRange();
    }
}