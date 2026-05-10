using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public ItemType allowedType;

    public void OnDrop(PointerEventData eventData) {
        // پیدا کردن آیتمی که در حال درگ شدن است
        InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (newItem != null) {
            // ۱. اول چک می‌کنیم که آیا نوع آیتم به این اسلات می‌خورد یا نه
            if (newItem.type == allowedType) {

                // ۲. چک می‌کنیم: آیا اسلات خالی است؟
                if (transform.childCount == 0) {
                    newItem.parentAfterDrag = transform;
                }
                // ۳. اگر اسلات پر بود، عملیات تعویض (Swap) را انجام می‌دهیم
                else {
                    // آیتمی که همین الان داخل اسلات هست را پیدا کن
                    Transform currentItemInSlot = transform.GetChild(0);
                    
                    // جای آیتم قدیمی را با جای قبلیِ آیتم جدید عوض کن
                    currentItemInSlot.SetParent(newItem.parentAfterDrag);
                    currentItemInSlot.localPosition = Vector3.zero;

                    // حالا آیتم جدید را در این اسلات بنشان
                    newItem.parentAfterDrag = transform;
                }
            }
        }
    }
}