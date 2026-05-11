using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyData enemyData;

    public CommonAttribute stats;

    void Start()
    {
        stats.Hp = enemyData.Hp;
        stats.Mana = enemyData.Mana;
        stats.Armor = enemyData.Armor;
        stats.ActionPoints = enemyData.ActionPoints;
        stats.Xp = enemyData.Xp;
    }
}