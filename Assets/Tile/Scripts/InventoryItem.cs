using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemType type; 
    
    //in parenti hast ke item bad az drag bayad dar un gharar begire.
    [HideInInspector] public Transform parentAfterDrag; 
    
    
    //bayad slot asli ro hamishe zakhire dashte bashim.
    private Transform originalSlot; 
    private CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        
        // 1.Save slot feli be onvan valed asli
        originalSlot = transform.parent;
        
       
        //2. be sorat pish farz, maghsad ra hamoon slot asli gharar midim.
        parentAfterDrag = originalSlot; 
        
        //3.joda kardan az slot baraye inke betone ruye baghie slot ha harekat kone 
        //va inke jeloye hame bashe.
        transform.SetParent(transform.parent.parent); 
        transform.SetAsLastSibling();
        
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
       
        //4.bazgasht be valedi ke nahayatan barash taeen shode (Ba slot ghabli ya jadid)
        transform.SetParent(parentAfterDrag);
        
        //5.Reset kardan mogheiat baraye gharar giri daghigh dar markaz slot
        transform.localPosition = Vector3.zero;
        
        canvasGroup.blocksRaycasts = true;
    }
}