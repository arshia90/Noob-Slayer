using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ItemType
{
    General,
    Weapon,
    Ring,
    Body,
    Head
}

[CreateAssetMenu]
public class itemData : ScriptableObject
{
   public int width = 1;
   public int height = 1;
   public Sprite itemIcon;


   public ItemType itemType = ItemType.General;
}
