using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Utilities/BaseSkill")]
public class BaseSkill : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;
    public int skillCost;
    public int skillDamage;
    public float skillCritChance;
    public int skillCooldown;
    //public int skillDuration;
    public SkillType skillType;
    public SkillTarget skillTarget;
    public CostType costType;
    public int costAmount;
    public GenerationType generationType;
    public int generationAmount;
    public SkillEffect[] skillEffects;
    public int[] effectDurations;
}
