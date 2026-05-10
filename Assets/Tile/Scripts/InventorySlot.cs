using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public ItemType allowedType;

    public void OnDrop(PointerEventData eventData) {
        InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (item != null && item.type == allowedType) {
            item.parentAfterDrag = transform;
        }
    }
}