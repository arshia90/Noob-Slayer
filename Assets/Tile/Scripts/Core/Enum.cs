using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Attack,
    Heal,
    Buff,
    Debuff
}

public enum SkillTarget
{
    Self,
    Ally,
    Enemy
}

public enum CostType
{
    None,
    Mana,
    Rage,
    Energy
}

public enum GenerationType
{
    None,
    Mana,
    Rage,
    Energy
}
public enum SkillEffect
{
    None,
    Damage,
    Bleed,
    Poison,
    Ignite,
    Freeze,
    Weakness,
    Defence,
    CritChance,
    CritDamage,
    Protection,
    HealingPower

}
