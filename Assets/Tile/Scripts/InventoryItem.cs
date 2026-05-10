using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemType type; 
    
    // این والدی است که آیتم بعد از درگ باید در آن قرار بگیرد
    [HideInInspector] public Transform parentAfterDrag; 
    
    // ما نیاز داریم اسلات اصلی را هم همیشه ذخیره داشته باشیم
    private Transform originalSlot; 
    private CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // ۱. ذخیره اسلات فعلی به عنوان والد اصلی
        originalSlot = transform.parent;
        
        // ۲. به صورت پیش‌فرض، مقصد را همان اسلات اصلی قرار می‌دهیم
        parentAfterDrag = originalSlot; 
        
        // ۳. جدا کردن از اسلات برای اینکه روی بقیه اسلات‌ها حرکت کند
        // ما این والد را به والدِ والد (یعنی Slots) تغییر می‌دهیم تا هم جلوی همه باشد و هم مختصاتش دیوانه نشود
        transform.SetParent(transform.parent.parent); 
        transform.SetAsLastSibling();
        
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        // ۴. بازگشت به والدی که نهایتاً برایش تعیین شده (یا اسلات قبلی یا اسلات جدید)
        transform.SetParent(parentAfterDrag);
        
        // ۵. ریست کردن موقعیت برای قرارگیری دقیق در مرکز اسلات
        transform.localPosition = Vector3.zero;
        
        canvasGroup.blocksRaycasts = true;
    }
}