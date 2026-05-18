using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class itemGrid : MonoBehaviour
{
    public const float tileSizeWidth = 96;
    public const float tileSizeHeight = 96;

    InventoryItem [,] inventoryItemSlot;
    RectTransform rectTransform;

    public int gridSizeWidth = 20;
    public int gridSizeHeight = 10;

   
   [SerializeField] private List<ItemType> allowedItemTypes = new List<ItemType>();

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth,gridSizeHeight);
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null) {return null;}

        CleanGridReference(toReturn);
        
        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)
    {
    for (int ix = 0; ix < item.itemData.width; ix++)
    {
        for (int iy = 0; iy < item.itemData.height; iy++)
        {
            inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
        }
    }
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2 (width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    private void Update()
    {
        //Debug.Log(Input.mousePosition);
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int(); 
    public  Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }


    public bool PlaceItem(InventoryItem inventoryitem, int posX, int posY,ref InventoryItem overlapItem)
    {

       if (allowedItemTypes.Count > 0 && !allowedItemTypes.Contains(ItemType.General) && !allowedItemTypes.Contains(inventoryitem.itemData.itemType))
        {
            return false; 
        }
        if( BoundryCheck(posX , posY, inventoryitem.itemData.width, inventoryitem.itemData.height) == false)
        {
            return false;
        }

        if (OverlapCheck(posX, posY, inventoryitem.itemData.width, inventoryitem.itemData.height, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;

        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }
        
        RectTransform rectTransform = inventoryitem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        for (int x = 0; x < inventoryitem.itemData.width; x++)
        {
            for (int y = 0; y < inventoryitem.itemData.height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryitem;
            }
        }

        inventoryitem.onGridPositionX = posX;
        inventoryitem.onGridPositionY = posY;
        

        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryitem.itemData.width / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryitem.itemData.height / 2 );

        rectTransform.localPosition = position;

        return true;
    }

  private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            if (inventoryItemSlot[posX + x, posY + y] != null)
            {
                if (overlapItem == null)
                {
                    overlapItem = inventoryItemSlot[posX + x, posY + y];
                }
                else
                {
                    if (overlapItem != inventoryItemSlot[posX + x, posY + y])
                    {
                        return false;
                    }
                }
            }
        }
    }

    return true;
    }   
    

    bool PositionCheck(int posX, int posY)
    {
        if(posX < 0 || posY < 0)
        {
            return false;
        }

        if (posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    bool BoundryCheck(int posX, int posY, int width, int height)
    {
        if(PositionCheck(posX,posY) == false) {return false;}

        posX += width -1;
        posY += height -1;

        if (PositionCheck(posX, posY) == false) { return false; }

        return true;
    
    }


}
