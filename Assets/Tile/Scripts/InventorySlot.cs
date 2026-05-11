using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public ItemType allowedType;

    public void OnDrop(PointerEventData eventData) 
    {
        //peida kardan itemi ke dar hale drag shodan ast.
        InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (newItem != null) 
        {
        
            // 1. aval check mikonim ke aya type item be in slot mikhore ya na.
            if (newItem.type == allowedType)
            {

                // 2.check mikonim aya slot khali hast ya na?
                if (transform.childCount == 0)
                {
                    newItem.parentAfterDrag = transform;
                }
                // 3.agar slot por bood,amaliat taviz (Swap) ra anjam midim.
                else 
                {
                    // itemi ke hamin alan dakhel slot hast ra por kon.
                    Transform currentItemInSlot = transform.GetChild(0);
                    
                    // jaye item ghadimi ro ba jaye ghabli item jadid avaz kon.
                    currentItemInSlot.SetParent(newItem.parentAfterDrag);
                    currentItemInSlot.localPosition = Vector3.zero;

                    // hala item ro dar in slot benshon
                    newItem.parentAfterDrag = transform;
                }
            }
        }
    }
}