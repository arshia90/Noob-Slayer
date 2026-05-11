using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerData playerData;

    public CommonAttribute stats;

    void Start()
    {
        stats.Hp = playerData.Hp;

        stats.Mana = playerData.Mana;

        stats.Armor = playerData.Armor;

        stats.ActionPoints = playerData.ActionPoints;

        stats.Xp = playerData.Xp;
    }
}