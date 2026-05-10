using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel; // اینجا باید همان Slots یا کانوست رو درگ کنی

    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            // اگر فعال بود غیرفعال میکنه، اگر غیرفعال بود فعال میکنه
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
        }
    }
}