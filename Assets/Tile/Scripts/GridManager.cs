using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{   
    public int columns = 8; // تعداد ستون‌های گریدت

    // این متد را برای پیدا کردن اسلات‌ها بر اساس مختصات نیاز داریم
    public GridSlot GetSlotAt(int targetX, int targetY)
    {
        GridSlot[] allSlots = GetComponentsInChildren<GridSlot>();
        foreach (var slot in allSlots)
        {
            if (slot.x == targetX && slot.y == targetY) return slot;
        }
        return null;
    }

    [ContextMenu("Assign Coordinates")]
    public void AssignCoords()
    {
        GridSlot[] slots = GetComponentsInChildren<GridSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].x = i % columns;
            slots[i].y = i / columns;
            slots[i].gameObject.name = $"Slot [{slots[i].x},{slots[i].y}]";
        }
    }
}
