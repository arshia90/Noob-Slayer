using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

// CHANGED: In khat tanzimin-e require component ezafe shode ast ta hatman CanvasGroup dashte bashim.
[RequireComponent(typeof(CanvasGroup))]
public class InventoryItem : MonoBehaviour
{
 
   public itemData itemData;

   public int onGridPositionX;
   public int onGridPositionY;

   // CHANGED: In moteghayer baraye dastresi be CanvasGroup ezafe shode ast.
   [HideInInspector] public CanvasGroup canvasGroup;

   // CHANGED: Tab-e Awake baraye gereftan-e reference CanvasGroup ezafe shode ast.
   private void Awake()
   {
       canvasGroup = GetComponent<CanvasGroup>();
       // Vghti item tuye grid hast, hatman blockade raycast ro true mizarim.
       canvasGroup.blocksRaycasts = true;
   }

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