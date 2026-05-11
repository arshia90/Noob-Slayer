using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;

    public int Hp;
    public int Mana;
    public int Armor;
    public int ActionPoints;
    public int Xp;
}