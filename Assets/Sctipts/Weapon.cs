using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float weaponDamage = 5f;
    public float WeaponDamage => weaponDamage;
}