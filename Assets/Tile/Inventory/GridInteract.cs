using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(itemGrid))]
public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   InventoryController inventoryController;
    itemGrid itemGrid;
   public void OnPointerEnter(PointerEventData eventData)
    {
        
        inventoryController.selectedItemGrid = itemGrid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        inventoryController.selectedItemGrid = null;
    }
  
    private void Awake()
    {
       inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
       itemGrid = GetComponent<itemGrid>();
    }
}
