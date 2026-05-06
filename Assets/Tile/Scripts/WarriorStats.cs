using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorStats : MonoBehaviour
{
   public CommonAttribute stats;

  public void Start()
    {
     
        stats.Hp = 100;
        stats.Mana = 100;
        stats.Armor = 100;
        stats.ActionPoints = 10;
        stats.Xp = 100;
    }

    void Update()
    {
        
    }
}
