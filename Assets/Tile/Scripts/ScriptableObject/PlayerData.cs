using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    public string playerName;

    public int Hp;

    public int Mana;

    public int Armor;

    public int ActionPoints;

    public int Xp;
}