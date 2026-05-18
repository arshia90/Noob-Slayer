using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI; // CHANGED: Ezafe shodan baraye GraphicRaycaster
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour
{
     public itemGrid selectedItemGrid;

     InventoryItem selectedItem;
     InventoryItem overlapItem;
     RectTransform rectTransform;

     [SerializeField] List<itemData> items;
     [SerializeField] GameObject itemPrefab;
     [SerializeField] Transform canvasTransform; 
    private void Update()
    {
        ItemIconDrag();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }


        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }    


        if (selectedItemGrid == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // ADDED: اگر روی اسلات‌های خالی اینونتوری یا منو کلیک شد و آیتمی دستت نبود، همینجا متوقف شو تا کلیک به پشت منو سرایت نکنه
                if (selectedItem == null)
                {
                    Debug.Log("Click ruye UI khord va motevaghef shod.");
                    return; 
                }
            }

            LeftMouseButtonPress();
        }       
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        
        // CHANGED: Vghti item jadid sakhte mishe, paretnesh ro miferestim be bome khonsa ta zire hichi nare!
        rectTransform.SetParent(canvasTransform);
        
        // CHANGED: Be mahzi ke item sakhte shod, BringItemToFront(true) ro seda mizanim ta biad ROOYE hameye UI ha.
        BringItemToFront(inventoryItem, true);

        int selectedItemID = Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }


    private void LeftMouseButtonPress()
    {

        Vector2 position = Input.mousePosition;

    if (selectedItem != null)
    {
        position.x -= (selectedItem.itemData.width - 1) * itemGrid.tileSizeWidth / 2;
        position.y += (selectedItem.itemData.height - 1) * itemGrid.tileSizeHeight / 2;
    }

         Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(position);
            

           if(selectedItem == null)
            {
              PickUpItem(tileGridPosition);
            }
                
            else
            {
                PlaceItem(tileGridPosition);
            }
    
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        // CHANGED: Ghabl az inke item ro zamin bezarim, Parent-esh ro motmaghteshe trasform parent e grid mikonim.
        selectedItem.GetComponent<RectTransform>().SetParent(selectedItemGrid.transform.parent);

        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if (complete)
        {
            // CHANGED: Vghti item ro ruye zamin mizarim, BringItemToFront(false) ro seda mizanim ta beshe doabre ruyesh click kard va sortingOrder adi beshe.
            BringItemToFront(selectedItem, false);

            selectedItem = null;
            if(overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                
                // CHANGED: Ghabl az inke item-e overlapping ro boland konim, parentesh ro moshakhas mikonim.
                rectTransform.SetParent(canvasTransform);

                // CHANGED: Agar item-e overlapping vojud dasht, BringItemToFront(true) ro seda mizanim.
                BringItemToFront(selectedItem, true);
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);

               if(selectedItem != null)
                {
                    rectTransform = selectedItem.GetComponent<RectTransform>();
                    
                    // CHANGED: Parent-e item ro be canvasTransform taghyir midim.
                    rectTransform.SetParent(canvasTransform);
                    
                    // CHANGED: دستور SetAsLastSibling تضمین می‌کند که این آیتم جدیداً فرزند آخرین والد است، بنابراین به طور طبیعی در آخرین مرحله رسم می‌شود.
                    rectTransform.SetAsLastSibling();

                    // CHANGED: Be mahzi ke item ro boland mikonim, BringItemToFront(true) ro seda mizanim ta biad ROOYE hameye UI ha neshon dade mishe.
                    BringItemToFront(selectedItem, true);
                }
    }
    
    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }

    // CHANGED: In tabe baraye bala ovordan va paeen bordane item-ha ba tanzimat-e Nested Canvas ezafe shode ast.
    private void BringItemToFront(InventoryItem item, bool toFront)
    {
        if (item == null) return;

        // Agar toFront true bashad:
        if (toFront)
        {
            // Ghabliat click shodan ro az item boland shode mighirad ta ruye grid e ziri click shavad.
            item.canvasGroup.blocksRaycasts = false;
            
            // Ye component-e Canvas (از نوع Nested) و GraphicRaycaster به صورت خودکار به آیتم اضافه می‌کنیم.
            Canvas itemCanvas = item.gameObject.AddComponent<Canvas>();
            item.gameObject.AddComponent<GraphicRaycaster>();
            
            // Tanzimat-e soritng order baraye bala ovordan
            itemCanvas.overrideSorting = true;
            itemCanvas.sortingOrder = 10000; // Ye adade kheili bala ta ghat'an biad ru.
        }
        else // Agar toFront false bashad:
        {
            // Ghabliat-e click shodan ro barmigardonim be item.
            item.canvasGroup.blocksRaycasts = true;
            
            // Component-haye nested Canvas va GraphicRaycaster ro pak mikonim ta tanzimat adi beshe.
            Destroy(item.gameObject.GetComponent<GraphicRaycaster>());
            Destroy(item.gameObject.GetComponent<Canvas>());
        }
    }
}