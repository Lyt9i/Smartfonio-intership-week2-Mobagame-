using UnityEngine;
public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    public Weapon GetWeapon()
    {
        return _weapon;
    }
}