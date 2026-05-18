using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
 
   public itemData itemData;

   public int onGridPositionX;
   public int onGridPositionY;

   internal void Set(itemData itemData)
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = itemData.width * itemGrid.tileSizeWidth;
        size.y = itemData.height * itemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }

}
