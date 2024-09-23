using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats.asset", menuName = "Weapon/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    public int damage;
    public int durability;
}
