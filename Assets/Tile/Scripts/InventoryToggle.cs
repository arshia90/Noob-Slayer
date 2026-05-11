using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel; // inja gameobject slots r drag mikonim.

    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            //age faal bood gheire faal mikone,Ya baraks.
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
        }
    }
}